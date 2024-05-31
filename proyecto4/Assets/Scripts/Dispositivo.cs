using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerRaycast : MonoBehaviour
{
    public float raycastDistance = 2.0f; // Distancia del raycast
    public Text interactText; // Referencia al texto de la UI
    public AudioClip victorySound; // Referencia al sonido de victoria
    private AudioSource audioSource; // Referencia al componente de AudioSource
    private bool isDeviceDeactivated = false; // Variable para verificar si el dispositivo ya ha sido desactivado

    void Start()
    {
        // Asegúrate de que el texto esté oculto al iniciar
        interactText.gameObject.SetActive(false);
        // Obtén el componente de AudioSource del objeto
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // No hacer nada si el dispositivo ya ha sido desactivado
        if (isDeviceDeactivated)
        {
            return;
        }

        // Lanza un raycast desde la posición del jugador hacia adelante
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            // Verifica si el objeto colisionado tiene el tag "Dispositivo"
            if (hit.collider.CompareTag("Dispositivo"))
            {
                // Muestra el texto en la UI
                interactText.gameObject.SetActive(true);

                // Verifica si se presiona la tecla 'F'
                if (Input.GetKeyDown(KeyCode.F))
                {
                    // Aquí puedes agregar la lógica para desactivar el dispositivo
                    Debug.Log("Dispositivo desactivado");
                    interactText.gameObject.SetActive(false); // Oculta el texto después de desactivar el dispositivo
                    isDeviceDeactivated = true; // Marca el dispositivo como desactivado
                    PlayVictorySoundAndChangeScene(); // Reproduce el sonido y cambia la escena
                }
            }
            else
            {
                // Oculta el texto si el objeto colisionado no tiene el tag "Dispositivo"
                interactText.gameObject.SetActive(false);
            }
        }
        else
        {
            // Oculta el texto si el raycast no colisiona con ningún objeto
            interactText.gameObject.SetActive(false);
        }
    }

    void PlayVictorySoundAndChangeScene()
    {
        audioSource.clip = victorySound;
        audioSource.Play();
        StartCoroutine(WaitForSoundToFinish());
    }

    System.Collections.IEnumerator WaitForSoundToFinish()
    {
        // Espera a que termine de reproducirse el sonido
        yield return new WaitForSeconds(audioSource.clip.length);
        // Cambia a la escena "Cinematica4"
        SceneManager.LoadScene("Cinematica4");
    }

    // Opcional: Visualiza el raycast en el editor para depuración
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * raycastDistance);
    }
}
