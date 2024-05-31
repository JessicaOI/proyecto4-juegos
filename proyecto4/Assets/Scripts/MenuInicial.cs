using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    void Start()
    {
        // Asegúrate de que el cursor esté visible y no bloqueado
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("_scene");
    }

    public void Salir()
    {
        Debug.Log("Salio");
        Application.Quit();
    }
}
