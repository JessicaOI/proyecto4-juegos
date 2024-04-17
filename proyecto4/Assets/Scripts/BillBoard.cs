using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Camera MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = MainCamera.transform.rotation;
    }
}
