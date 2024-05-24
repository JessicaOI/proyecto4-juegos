using UnityEngine;

public class BrujulaFuncional : MonoBehaviour
{
    // Referencia al objeto de la aguja de la brújula
    public RectTransform needle;
    // Tag de los objetos enemigos
    public string enemyTag = "Enemy";
    // Referencia a la cámara del jugador
    public Camera playerCamera;

    void Update()
    {
        // Encuentra el enemigo más cercano
        Transform nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            // Calcula la dirección desde la cámara al enemigo más cercano
            Vector3 direction = nearestEnemy.position - playerCamera.transform.position;
            // Calcula el ángulo en el plano XZ relativo a la cámara
            Vector3 cameraForward = playerCamera.transform.forward;
            cameraForward.y = 0;
            direction.y = 0;
            float angle = Vector3.SignedAngle(cameraForward, direction, Vector3.up);
            // Ajusta la rotación de la aguja
            needle.localRotation = Quaternion.Euler(0, 0, -angle);
        }
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = playerCamera.transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, currentPosition);
            if (distance < minDistance)
            {
                nearestEnemy = enemy.transform;
                minDistance = distance;
            }
        }
        return nearestEnemy;
    }
}
