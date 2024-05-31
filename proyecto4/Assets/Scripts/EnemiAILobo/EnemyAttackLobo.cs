using UnityEngine;

public class EnemyAttackLobo : MonoBehaviour
{
    public int damage = 20;
    public float attackRange = 2.0f;
    public LayerMask playerLayer;

    void Update()
    {
        // Empty update, as attacks are handled by the EnemyAi script
    }

    public void PerformAttack()
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
