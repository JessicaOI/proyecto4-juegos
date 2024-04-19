using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    void OnEnable() 
    {
        SceneManager.LoadScene("Scene", LoadSceneMode.Single);
    }
}
