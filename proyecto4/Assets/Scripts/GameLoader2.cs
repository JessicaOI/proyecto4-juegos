using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader2 : MonoBehaviour
{
    void OnEnable() 
    {
        MenuInicial.Cinematica2Completada();
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}

