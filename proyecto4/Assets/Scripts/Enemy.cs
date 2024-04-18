using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Image farImage;
    public Image closeImage;
    public Image veryCloseImage;

    public Transform playerTransform;

    public float farDistance = 10f;
    public float closeDistance = 5f;
    public float moveSpeed = 1f; // Velocidad de movimiento al estar close
    public float chargeSpeed = 5f; // Velocidad de embestida al estar very close
    public float chargeDelay = 1f; // Tiempo de espera entre embestidas

    private enum EnemyState
    {
        Far,
        Close,
        VeryClose,
        Attacking
    }

    private EnemyState currentState;
    private Vector3 originalPosition;
    private float timeSinceLastCharge;

    void Start()
    {
        currentState = EnemyState.Far;
        originalPosition = transform.position;
        timeSinceLastCharge = 0f;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        UpdateEnemyState(distanceToPlayer);
        ExecuteCurrentState();
    }

    private void UpdateEnemyState(float distanceToPlayer)
    {
        if (distanceToPlayer > farDistance)
        {
            currentState = EnemyState.Far;
        }
        else if (distanceToPlayer > closeDistance)
        {
            currentState = EnemyState.Close;
        }
        else if (distanceToPlayer > 1f)
        {
            currentState = EnemyState.VeryClose;
        }
        else
        {
            currentState = EnemyState.Attacking;
        }
    }

    private void ExecuteCurrentState()
    {
        switch (currentState)
        {
            case EnemyState.Far:
                WanderRandomly();
                farImage.gameObject.SetActive(true);
                closeImage.gameObject.SetActive(false);
                veryCloseImage.gameObject.SetActive(false);
                break;
            case EnemyState.Close:
                MoveTowardsPlayerSlowly();
                farImage.gameObject.SetActive(false);
                closeImage.gameObject.SetActive(true);
                veryCloseImage.gameObject.SetActive(false);
                break;
            case EnemyState.VeryClose:
                ChargeAtPlayer();
                farImage.gameObject.SetActive(false);
                closeImage.gameObject.SetActive(false);
                veryCloseImage.gameObject.SetActive(true);
                break;
            case EnemyState.Attacking:
                // Implementa lógica de ataque repetitivo aquí
                AttackRepeatedly();
                break;
        }
    }

    private void WanderRandomly()
    {
        Vector3 randomDirection = Random.insideUnitSphere * moveSpeed;
        randomDirection.y = 0;
        transform.Translate(randomDirection * Time.deltaTime);
    }

    private void MoveTowardsPlayerSlowly()
    {
        transform.LookAt(playerTransform);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void ChargeAtPlayer()
    {
        transform.LookAt(playerTransform);
        transform.position += transform.forward * chargeSpeed * Time.deltaTime;
    }

    private void AttackRepeatedly()
    {
        // Implementa lógica de ataque repetitivo aquí
        // Puedes usar un temporizador para controlar el ritmo de ataque
        timeSinceLastCharge += Time.deltaTime;
        if (timeSinceLastCharge >= chargeDelay)
        {
            timeSinceLastCharge = 0f;
            // Implementa el ataque aquí, por ejemplo:
            // playerHealth.TakeDamage(damageAmount);
        }
    }
}
