using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject startScreen; // Asigna tu imagen/canvas aquí desde el editor
    private bool firstTime = true;

    void Start()
    {
        if (firstTime)
        {
            Time.timeScale = 0; // Detiene el tiempo en el juego, pausando efectivamente todos los scripts que dependen de Update con deltaTime
            startScreen.SetActive(true); // Mostrar la pantalla de inicio
            DisableAllScripts(); // Desactivar todos los scripts de la escena
        }
    }

    void Update()
    {
        if (firstTime && Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1; // Reanuda el tiempo y los scripts pausados
            startScreen.SetActive(false); // Ocultar la pantalla de inicio
            EnableAllScripts(); // Activar todos los scripts de la escena
            firstTime = false; // Asegura que esto solo suceda la primera vez
        }
    }

    void DisableAllScripts()
    {
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this) // Evitar desactivar este script
                script.enabled = false;
        }
    }

    void EnableAllScripts()
    {
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
    }
}
