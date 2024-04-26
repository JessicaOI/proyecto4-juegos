using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image redImage; // Referencia a la imagen roja de la UI

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthImage();
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
        // Aquí puedes añadir cualquier efecto de muerte que quieras para el jugador
        Debug.Log("Player has died!");
        // Opcionalmente, destruye el objeto jugador si es necesario
        // Destroy(gameObject);
    }
}
