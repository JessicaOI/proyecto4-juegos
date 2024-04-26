using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 20;
    public float attackRange = 2.0f;
    public LayerMask playerLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Suponemos que el golpe se activa con la barra espaciadora
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, playerLayer))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Damage applied: " + damage);
            }
            else
            {
                Debug.Log("PlayerHealth component not found on hit object.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any object in the player layer.");
        }
    }

}
