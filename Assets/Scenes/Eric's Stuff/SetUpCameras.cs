using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpCameras : MonoBehaviour
{
    public Camera MainCamera;
    public Camera StaticCamera1;
    public Camera StaticCamera2;


    void Start()
    {
        MainCamera.enabled = true;
        StaticCamera1.enabled = false;
        StaticCamera2.enabled = false;
    }
}
