using UnityEngine;
using System.Collections;

public class MouseLookScript : MonoBehaviour
{
    [HideInInspector]
    public Transform myCamera;
    [Tooltip("Object to be thrown")]
    public GameObject throwableItem;
    [Tooltip("Force with which the item will be thrown")]
    public float throwForce = 10f;

    [Tooltip("Tag de los objetos enemigos")]
    public string enemyTag = "Bear";
    [Tooltip("Distancia máxima para activar la imagen de alerta")]
    public float maxDistance = 5f;
    [Tooltip("Referencia a la imagen de alerta en el Canvas")]
    public GameObject alertImage;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        myCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        if (myCamera == null)
        {
            Debug.LogError("MainCamera not found. Make sure the camera is tagged as 'MainCamera'.");
        }
        if (alertImage == null)
        {
            Debug.LogError("AlertImage is not assigned. Make sure to assign the AlertImage in the Inspector.");
        }
    }

    void Update()
    {
        MouseInputMovement();
        if (Input.GetKeyDown(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ThrowItem();
        }
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        if (GetComponent<PlayerMovementScript>().currentSpeed > 1)
            HeadMovement();

        CheckEnemyDistance();
    }

    private void ThrowItem()
    {
        if (throwableItem != null)
        {
            GameObject item = Instantiate(throwableItem, myCamera.position, myCamera.rotation);
            Rigidbody itemRigidbody = item.AddComponent<Rigidbody>();
            itemRigidbody.AddForce(myCamera.forward * throwForce, ForceMode.VelocityChange);
        }
    }

    private void CheckEnemyDistance()
    {
        Transform nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            Debug.Log("Nearest enemy found: " + nearestEnemy.name);

            float distanceToEnemy = Vector3.Distance(nearestEnemy.position, myCamera.position);
            Debug.Log("Distance to nearest enemy: " + distanceToEnemy);

            if (distanceToEnemy > maxDistance)
            {
                Debug.Log("Enemy is beyond max distance. Activating alert image.");
                alertImage.SetActive(true);
            }
            else
            {
                Debug.Log("Enemy is within max distance. Deactivating alert image.");
                alertImage.SetActive(false);
            }
        }
        else
        {
            Debug.Log("No enemies found. Deactivating alert image.");
            alertImage.SetActive(false);
        }
    }

    private Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if (enemies.Length == 0)
        {
            Debug.Log("No enemies with tag " + enemyTag + " found.");
            return null;
        }

        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = myCamera.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, currentPosition);
            if (distance < minDistance)
            {
                nearestEnemy = enemy.transform;
                minDistance = distance;
            }
        }

        Debug.Log("Nearest enemy is " + nearestEnemy.name + " at distance " + minDistance);
        return nearestEnemy;
    }

    [Header("Z Rotation Camera")]
    [HideInInspector] public float timer;
    [HideInInspector] public int int_timer;
    [HideInInspector] public float zRotation;
    [HideInInspector] public float wantedZ;
    [HideInInspector] public float timeSpeed = 2;

    [HideInInspector] public float timerToRotateZ;
    void HeadMovement()
    {
        timer += timeSpeed * Time.deltaTime;
        int_timer = Mathf.RoundToInt(timer);
        if (int_timer % 2 == 0)
        {
            wantedZ = -1;
        }
        else
        {
            wantedZ = 1;
        }

        zRotation = Mathf.Lerp(zRotation, wantedZ, Time.deltaTime * timerToRotateZ);
    }

    [Tooltip("Current mouse sensivity, changes in the weapon properties")]
    public float mouseSensitvity = 0;
    [HideInInspector]
    public float mouseSensitvity_notAiming = 300;
    [HideInInspector]
    public float mouseSensitvity_aiming = 50;

    void FixedUpdate()
    {
        if (Input.GetAxis("Fire2") != 0)
        {
            mouseSensitvity = mouseSensitvity_aiming;
        }
        else if (GetComponent<PlayerMovementScript>().maxSpeed > 5)
        {
            mouseSensitvity = mouseSensitvity_notAiming;
        }
        else
        {
            mouseSensitvity = mouseSensitvity_notAiming;
        }

        ApplyingStuff();
    }

    private float rotationYVelocity, cameraXVelocity;
    [Tooltip("Speed that determines how much camera rotation will lag behind mouse movement.")]
    public float yRotationSpeed, xCameraSpeed;

    [HideInInspector]
    public float wantedYRotation;
    [HideInInspector]
    public float currentYRotation;

    [HideInInspector]
    public float wantedCameraXRotation;
    [HideInInspector]
    public float currentCameraXRotation;

    [Tooltip("Top camera angle.")]
    public float topAngleView = 60;
    [Tooltip("Minimum camera angle.")]
    public float bottomAngleView = -45;

    void MouseInputMovement()
    {
        wantedYRotation += Input.GetAxis("Mouse X") * mouseSensitvity;
        wantedCameraXRotation -= Input.GetAxis("Mouse Y") * mouseSensitvity;
        wantedCameraXRotation = Mathf.Clamp(wantedCameraXRotation, bottomAngleView, topAngleView);
    }

    void ApplyingStuff()
    {
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, yRotationSpeed);
        currentCameraXRotation = Mathf.SmoothDamp(currentCameraXRotation, wantedCameraXRotation, ref cameraXVelocity, xCameraSpeed);

        WeaponRotation();

        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        myCamera.localRotation = Quaternion.Euler(currentCameraXRotation, 0, zRotation);
    }

    private Vector2 velocityGunFollow;
    private float gunWeightX, gunWeightY;
    [Tooltip("Current weapon that player carries.")]
    [HideInInspector]
    public GameObject weapon;
    private GunScript gun;

    void WeaponRotation()
    {
        if (!weapon)
        {
            weapon = GameObject.FindGameObjectWithTag("Weapon");
            if (weapon)
            {
                if (weapon.GetComponent<GunScript>())
                {
                    try
                    {
                        gun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<GunScript>();
                    }
                    catch (System.Exception ex)
                    {
                        print("gun not found->" + ex.StackTrace.ToString());
                    }
                }
            }
        }
    }

    float deltaTime = 0.0f;
    [Tooltip("Shows FPS in top left corner.")]
    public bool showFps = true;

    void OnGUI()
    {
        if (showFps)
        {
            FPSCounter();
        }
    }

    void FPSCounter()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = Color.white;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
