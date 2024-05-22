using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader3 : MonoBehaviour
{
    void OnEnable() 
    {
        SceneManager.LoadScene("Nivel2", LoadSceneMode.Single);
    }
}
