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

    // Audio
    private AudioSource attackAudioSource;
    private AudioSource chaseAudioSource; // Nuevo AudioSource para persecución

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();

        // Obtener ambos AudioSource
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length > 1)
        {
            attackAudioSource = audioSources[0];
            chaseAudioSource = audioSources[1];
        }
        else
        {
            Debug.LogError("Not enough AudioSources attached to the enemy.");
        }
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
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            // Rotate towards the walk point smoothly
            Vector3 direction = (walkPoint - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Move towards walk point if facing it closely enough
            if (Vector3.Angle(transform.forward, direction) < 10f)
            {
                agent.SetDestination(walkPoint);
                myAnim.SetBool("WalkForward", true);
            }
            else
            {
                myAnim.SetBool("WalkForward", false);
            }
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Attempt to find a point in front of the enemy
        float randomZ = Random.Range(0, walkPointRange); // Changed to 0 to walkPointRange to avoid backward movement
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

        // Play chase sound if not already playing
        if (chaseAudioSource != null && !chaseAudioSource.isPlaying)
        {
            chaseAudioSource.Play();
        }
    }

    private void AttackPlayer()
    {
        // Ensure the enemy doesn't move while attacking
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            myAnim.SetTrigger("Attack1");

            // Play attack sound with debug messages
            if (attackAudioSource != null)
            {
                if (attackAudioSource.clip != null)
                {
                    attackAudioSource.Play();
                    Debug.Log("Attack audio played.");
                }
                else
                {
                    Debug.LogWarning("Attack audio source is assigned but no audio clip found.");
                }
            }
            else
            {
                Debug.LogWarning("No audio source assigned to attackAudioSource.");
            }

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
