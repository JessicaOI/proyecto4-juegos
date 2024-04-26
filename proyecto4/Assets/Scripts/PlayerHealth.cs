using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image redImage; // Referencia a la imagen roja de la UI
    public GameObject deathText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthImage();
        deathText.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que la salud no sea menor que 0
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthImage();
    }

    void UpdateHealthImage()
    {
        float opacity = 1.0f - ((float)currentHealth / maxHealth); // Calcula la nueva opacidad basada en la salud restante
        redImage.color = new Color(redImage.color.r, redImage.color.g, redImage.color.b, opacity);
    }

    void Die()
    {
        Debug.Log("Player has died!");
        deathText.SetActive(true);
        StartCoroutine(ReloadSceneAfterDelay(3f)); // Inicia la corutina con un retraso de 3 segundos
    }

    System.Collections.IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo definido
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual
    }
}
