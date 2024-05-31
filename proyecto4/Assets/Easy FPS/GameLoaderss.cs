using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoaderss : MonoBehaviour
{
    void Update()
    {
        // Verifica si se ha presionado la tecla "Esc"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Cambia a la escena llamada "Menu"
            SceneManager.LoadScene("Menu");
        }
    }
}
