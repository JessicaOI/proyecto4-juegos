using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public bool PlayerInSightRange { get { return playerInSightRange; } }
    public bool PlayerInAttackRange { get { return playerInAttackRange; } }


    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Animator myAnim;

    // Sound Response
    public bool isRespondingToSound = false;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isRespondingToSound)
        {
            if (Vector3.Distance(transform.position, agent.destination) < 1f)
            {
                isRespondingToSound = false; // Reset when the sound point is reached
                myAnim.SetBool("WalkForward", true); // Ensure the walking animation stops
            }
            return; // Skip the rest of the update while responding to sound
        }

        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        myAnim.ResetTrigger("Attack1");
        myAnim.SetBool("WalkForward", true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        myAnim.SetBool("WalkForward", true);
        myAnim.ResetTrigger("Attack1");
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Ensure the enemy doesn't move while attacking
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            myAnim.SetTrigger("Attack1");

            // Get the PlayerHealth component and apply damage
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(20); // Assuming 20 is the damage value you want to inflict
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on the Player.");
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void RespondToSound(Vector3 soundPosition)
    {
        isRespondingToSound = true;
        agent.SetDestination(soundPosition);
        myAnim.SetBool("WalkForward", true); // Make sure to play the walk animation
    }
}
