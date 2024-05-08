using GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FunctionalAdult : MonoBehaviour, IHear
{
    [SerializeField] private NavMeshAgent agent = null;

    void Awake()
    {
        if (agent == null && !TryGetComponent(out agent))
            Debug.LogWarning(name + " doesn't have an agent!");
    }

    public void RespondToSound(Sound sound)
    {
        /* 
        *   Put fun things here
        *   Examples:
        *   Animate the NPC, Play a sound clip ("What was that?!"), Throw some UI up, Check if the sound is more important than current task
        */

        if (sound.soundType == Sound.SoundType.Interesting)
        {
            // Check if EnemyAi is present and change the state
            var enemyAI = GetComponent<EnemyAi>();
            if (enemyAI != null)
            {
                enemyAI.RespondToSound(sound.pos);
            }
            else
            {
                Debug.LogWarning("EnemyAi component not found on the GameObject.");
            }
        }
    }
}
