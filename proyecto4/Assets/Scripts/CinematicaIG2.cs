using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class CinematicaIG2 : MonoBehaviour
{
    public GameObject player;
    public GameObject objectToActivate;
    public GameObject[] objectsInCinematic; // Array de objetos a destruir después de la cinemática
    public GameObject[] objectsToDeactivate; // Array de objetos a desactivar durante la cinemática
    public GameObject objectToShowAfterCinematic; // Objeto a activar después de la cinemática
    public AudioSource audioSource; // Fuente de audio para reproducir la canción

    private void Start()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
        else
        {
            Debug.LogWarning("objectToActivate no está asignado.");
        }

        if (player == null)
        {
            Debug.LogWarning("player no está asignado.");
        }

        if (objectToShowAfterCinematic != null)
        {
            objectToShowAfterCinematic.SetActive(false);
        }
        else
        {
            Debug.LogWarning("objectToShowAfterCinematic no está asignado.");
        }

        if (audioSource == null)
        {
            Debug.LogWarning("audioSource no está asignado.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectado: " + other.gameObject.name);

        if (other.gameObject == player)
        {
            Debug.Log("El jugador ha entrado en el trigger.");

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
                Debug.Log("Objeto activado: " + objectToActivate.name);

                // Desactivar objetos durante la cinemática
                foreach (GameObject obj in objectsToDeactivate)
                {
                    if (obj != null)
                    {
                        obj.SetActive(false);
                        Debug.Log("Objeto desactivado: " + obj.name);
                    }
                }

                PlayableDirector playableDirector = objectToActivate.GetComponent<PlayableDirector>();
                if (playableDirector != null)
                {
                    playableDirector.Play();
                    Debug.Log("Cinemática iniciada.");

                    playableDirector.stopped += OnPlayableDirectorStopped;
                }
                else
                {
                    Debug.LogError("No se encontró PlayableDirector en el objeto activado.");
                }
            }
            else
            {
                Debug.LogError("objectToActivate no está asignado.");
            }
        }
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        Debug.Log("Cinemática finalizada. Destruyendo objetos de la cinemática.");

        foreach (GameObject obj in objectsInCinematic)
        {
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log("Objeto destruido: " + obj.name);
            }
        }

        // Reactivar objetos después de la cinemática
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
                Debug.Log("Objeto reactivado: " + obj.name);
            }
        }

        // Opcionalmente, desactivar el objeto de la cinemática después de su uso
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
            Debug.Log("Objeto desactivado: " + objectToActivate.name);
        }

        // Activar el objeto y reproducir la canción
        if (objectToShowAfterCinematic != null)
        {
            objectToShowAfterCinematic.SetActive(true);
            Debug.Log("Objeto activado después de la cinemática: " + objectToShowAfterCinematic.name);
        }

        if (audioSource != null)
        {
            audioSource.Play();
            Debug.Log("Canción reproducida.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión detectada: " + collision.gameObject.name);
    }
}
