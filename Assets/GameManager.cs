using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public int cameraNumber = 1;

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameraNumber = 0;
            cameraManager.ChangeCameraView(cameraNumber);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraNumber = 1;
            cameraManager.ChangeCameraView(cameraNumber);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cameraNumber = 2;
            cameraManager.ChangeCameraView(cameraNumber);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cameraNumber = 3;
            cameraManager.ChangeCameraView(cameraNumber);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            cameraNumber = 4;
            cameraManager.ChangeCameraView(cameraNumber);
        }
    }
}
