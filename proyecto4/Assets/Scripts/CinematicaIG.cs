using UnityEngine;
using UnityEngine.Playables;

public class CinematicaIG : MonoBehaviour
{
    public GameObject player; 
    public GameObject objectToActivate; 

    private void Start()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("a" + other.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Detecto");

        if (other.gameObject == player)
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);


                PlayableDirector playableDirector = objectToActivate.GetComponent<PlayableDirector>();
                if (playableDirector != null)
                {
                    playableDirector.Play();
                }
            }
        }
    }
}
