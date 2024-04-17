using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarFill; // Referencia a la imagen verde de la barra de vida

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        healthBarFill.fillAmount = fillAmount;
    }

    void Die()
    {
        // Aquí puedes añadir cualquier efecto de muerte que quieras para el enemigo
        Destroy(gameObject);
    }
}
