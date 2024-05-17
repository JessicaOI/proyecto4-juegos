using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader2 : MonoBehaviour
{
    void OnEnable() 
    {
        SceneManager.LoadScene("Menu2", LoadSceneMode.Single);
    }
}

