using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BearsCount : MonoBehaviour
{

    public TextMeshProUGUI dummieCountText; // Usa esta variable para referenciar tu componente TextMeshProUGUI

    void Update()
    {
        int count = GameObject.FindGameObjectsWithTag("Dummie").Length;
        dummieCountText.text = "Osos: " + count;
    }
}



