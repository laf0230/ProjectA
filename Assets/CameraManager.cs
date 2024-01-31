using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cameras;

    public void ChangeCameraView(int number)
    {
        foreach (var cam in cameras)
        {
            cam.Priority = 10;
        }
        cameras[number].Priority = 11;
    }
}
