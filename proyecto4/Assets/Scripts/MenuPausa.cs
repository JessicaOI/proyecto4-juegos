using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private Button botonReanudar;
    [SerializeField] private Button botonReiniciar;
    [SerializeField] private Button botonCerrar;

    private bool juegopausado = false;

    private void Start()
    {
        // Configurar eventos de los botones
        botonReanudar.onClick.AddListener(Reanudar);
        botonReiniciar.onClick.AddListener(Reiniciar);
        botonCerrar.onClick.AddListener(Cerrar);
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegopausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }
    
    public void Pausa()
    {
        // Pausar otros scripts antes de mostrar el menú de pausa
        PausarScripts();

        // Mostrar el menú de pausa después de pausar otros scripts
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);

        // Establecer el estado de pausa del juego
        juegopausado = true;
    }

    private void PausarScripts()
    {
        // Obtener todos los scripts en la escena
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();

        // Iterar sobre los scripts y desactivarlos, excepto este script y el de la cámara
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this && script.GetType() != typeof(Camera))
            {
                script.enabled = false;
            }
        }
    }

    public void Reanudar()
    {
        // Reanudar otros scripts antes de ocultar el menú de pausa
        ReanudarScripts();

        // Ocultar el menú de pausa después de reanudar otros scripts
        menuPausa.SetActive(false);
        botonPausa.SetActive(true);

        // Establecer el estado de pausa del juego
        juegopausado = false;
    }

    private void ReanudarScripts()
    {
        // Obtener todos los scripts en la escena
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();

        // Iterar sobre los scripts y activarlos, excepto este script y el de la cámara
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this && script.GetType() != typeof(Camera))
            {
                script.enabled = true;
            }
        }
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Cerrar()
    {
        Application.Quit();
    }
}
