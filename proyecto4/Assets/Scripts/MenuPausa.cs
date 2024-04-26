using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    // Referencia al menú de pausa UI
    public GameObject menuPausaUI;
    public static bool JuegoPausado = false;


    void Update()
    {
        // Detectar cuando el jugador presiona la tecla Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (JuegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        JuegoPausado = false;

        // Ocultar y bloquear el cursor al reanudar el juego
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pausar()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        JuegoPausado = true;

        // Mostrar y desbloquear el cursor al pausar el juego
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    // Función para reiniciar la escena
    public void Reiniciar()
    {
        // Carga la escena actualmente activa (reinicia)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Asegurarse de reanudar el tiempo del juego
        JuegoPausado = false; // Restablecer la pausa
    }

    // Función para salir del juego
    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

}
