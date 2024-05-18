using UnityEngine;

public class ToggleComponentWithSound : MonoBehaviour
{
    // El componente que quieres activar/desactivar
    public MonoBehaviour componentToToggle;

    // El AudioSource que reproducirá el sonido
    public AudioSource audioSource;

    // El clip de audio que se reproducirá
    public AudioClip toggleSound;

    void Update()
    {
        // Verifica si la tecla "T" fue presionada
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Activa/desactiva el componente
            componentToToggle.enabled = !componentToToggle.enabled;

            // Reproduce el sonido si hay un AudioSource y un clip de audio asignado
            if (audioSource != null && toggleSound != null)
            {
                audioSource.PlayOneShot(toggleSound);
            }
        }
    }
}
