using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BearsCount : MonoBehaviour
{
    public TextMeshProUGUI dummieCountText; // Usa esta variable para referenciar tu componente TextMeshProUGUI
    public AudioClip soundClip; // Sonido que se reproducirá cuando el contador llegue a 0
    public string nextSceneName; // Nombre de la siguiente escena
    private AudioSource audioSource;

    void Start()
    {
        // Añade un componente AudioSource a este GameObject si no tiene uno
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;
    }

    void Update()
    {
        int count = GameObject.FindGameObjectsWithTag("Dummie").Length;
        dummieCountText.text = "Osos: " + count;

        if (count == 0)
        {
            StartCoroutine(PlaySoundAndChangeScene());
        }
    }

    IEnumerator PlaySoundAndChangeScene()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            // Espera a que el sonido termine de reproducirse
            yield return new WaitForSeconds(audioSource.clip.length);
            // Cambia a la siguiente escena
            SceneManager.LoadScene(nextSceneName);
        }
    }
}




