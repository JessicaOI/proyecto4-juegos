using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader4 : MonoBehaviour
{
    void OnEnable() 
    {
        SceneManager.LoadScene("Menu3", LoadSceneMode.Single);
    }
}
