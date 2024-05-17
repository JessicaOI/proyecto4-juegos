using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial2 : MonoBehaviour
{
    [SerializeField] private GameObject panelSeleccionNiveles;
    [SerializeField] private Button botonSeleccionNiveles;
    [SerializeField] private Button botonNivel1;
    [SerializeField] private Button botonNivel2;
    [SerializeField] private Button botonNivel3;
    [SerializeField] private Button botonCerrarPanel;

    void Start()
    {
        panelSeleccionNiveles.SetActive(false);

        botonSeleccionNiveles.onClick.AddListener(AbrirPanelSeleccionNiveles);
        botonNivel1.onClick.AddListener(() => CargarNivel(1));
        botonNivel2.onClick.AddListener(() => CargarNivel(2));
        botonNivel3.onClick.AddListener(() => CargarNivel(3));
        botonCerrarPanel.onClick.AddListener(CerrarPanelSeleccionNiveles);

        // Ajustes iniciales del cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // El cursor es libre para moverse
    }

    private void AbrirPanelSeleccionNiveles()
    {
        panelSeleccionNiveles.SetActive(true);
    }

    private void CerrarPanelSeleccionNiveles()
    {
        panelSeleccionNiveles.SetActive(false);
    }

    private void CargarNivel(int nivelIndex)
    {
        SceneManager.LoadScene("Nivel" + nivelIndex);
    }

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Salio");
        Application.Quit();
    }
}
