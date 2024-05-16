using System.Collections.Generic;
using UnityEngine;

public class RadarOso : MonoBehaviour
{
    public GameObject greenIndicator;
    public GameObject yellowIndicator;
    public GameObject redIndicator;

    public Transform player;
    private List<EnemyAi> enemies = new List<EnemyAi>();

    void Start()
    {
        FindAllEnemies();
    }

    void Update()
    {
        bool anyInSightRange = false;
        bool anyInAttackRange = false;

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            float distance = Vector3.Distance(player.position, enemy.transform.position);
            if (distance <= enemy.sightRange)
            {
                anyInSightRange = true;

                if (distance <= enemy.attackRange)
                {
                    anyInAttackRange = true;
                    break;  // Prioriza el estado de ataque si algún enemigo está suficientemente cerca
                }
            }
        }

        UpdateIndicators(anyInSightRange, anyInAttackRange);
    }

    private void FindAllEnemies()
    {
        enemies.Clear();
        EnemyAi[] enemiesArray = FindObjectsOfType<EnemyAi>();
        foreach (var enemy in enemiesArray)
        {
            enemies.Add(enemy);
        }
    }

    private void UpdateIndicators(bool inSightRange, bool inAttackRange)
    {
        greenIndicator.SetActive(!inSightRange && !inAttackRange);
        yellowIndicator.SetActive(inSightRange && !inAttackRange);
        redIndicator.SetActive(inAttackRange);
    }
}
