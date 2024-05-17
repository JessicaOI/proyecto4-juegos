using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private GameObject panelSeleccionNiveles; // El panel que contiene los botones de selección de niveles
    [SerializeField] private Button botonSeleccionNiveles; // El botón que abre el panel de selección de niveles
    [SerializeField] private Button botonNivel1; // Botón para el nivel 1
    [SerializeField] private Button botonNivel2; // Botón para el nivel 2
    [SerializeField] private Button botonNivel3; // Botón para el nivel 3
    [SerializeField] private Button botonCerrarPanel; // Botón para cerrar el panel

    private const string Cinematica2CompletadaKey = "Cinematica2Completada"; // La clave para guardar el estado de la cinemática

    void Start()
    {
        // Asegurarse de que el panel esté inicialmente desactivado
        panelSeleccionNiveles.SetActive(false);

        // Configurar eventos de los botones
        botonSeleccionNiveles.onClick.AddListener(AbrirPanelSeleccionNiveles);
        botonNivel1.onClick.AddListener(() => CargarNivel(1));
        botonNivel2.onClick.AddListener(() => CargarNivel(2));
        botonNivel3.onClick.AddListener(() => CargarNivel(3));
        botonCerrarPanel.onClick.AddListener(CerrarPanelSeleccionNiveles);

        // Verificar si la cinemática ha sido completada
        if (PlayerPrefs.GetInt(Cinematica2CompletadaKey, 0) == 1)
        {
            botonSeleccionNiveles.gameObject.SetActive(true);
        }
        else
        {
            botonSeleccionNiveles.gameObject.SetActive(false);
        }

        // Asegurarse de que el cursor esté visible y desbloqueado
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Jugar()
    {
        // Restablecer PlayerPrefs cuando se inicia un nuevo juego
        PlayerPrefs.SetInt(Cinematica2CompletadaKey, 0);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Salio");
        Application.Quit();
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
        SceneManager.LoadScene("Nivel" + nivelIndex); // Suponiendo que los niveles se llamen "Nivel1", "Nivel2", "Nivel3", etc.
    }

    // Este método debe ser llamado cuando la cinemática 2 se complete
    public static void Cinematica2Completada()
    {
        PlayerPrefs.SetInt(Cinematica2CompletadaKey, 1);
        PlayerPrefs.Save();
    }
}
