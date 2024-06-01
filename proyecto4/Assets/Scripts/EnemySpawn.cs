using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Referencia al prefab del enemigo que queremos spawnear
    public GameObject enemyPrefab;
    // Tiempo en segundos entre cada spawn
    public float spawnInterval = 2.0f;
    // Tiempo para el primer spawn
    public float initialDelay = 1.0f;
    // Controlar si el spawner está activo
    public bool isActive = true;

    private void Start()
    {
        // Iniciar el spawner
        InvokeRepeating("SpawnEnemy", initialDelay, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (isActive)
        {
            // Crear una nueva instancia del enemigo en la posición y rotación del spawner
            Instantiate(enemyPrefab, transform.position, transform.rotation);
        }
    }

    // Función para activar/desactivar el spawner
    public void SetActive(bool active)
    {
        isActive = active;
    }
}
