using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    void Start()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        // 수평 이동 제한
        if (freeLookCamera.m_XAxis.m_InputAxisValue < 0 && transform.position.x <= minX)
        {
            freeLookCamera.m_XAxis.m_InputAxisValue = 0;
        }
        else if (freeLookCamera.m_XAxis.m_InputAxisValue > 0 && transform.position.x >= maxX)
        {
            freeLookCamera.m_XAxis.m_InputAxisValue = 0;
        }

        // 수직 이동 제한
        if (freeLookCamera.m_YAxis.m_InputAxisValue < 0 && transform.position.y <= minY)
        {
            freeLookCamera.m_YAxis.m_InputAxisValue = 0;
        }
        else if (freeLookCamera.m_YAxis.m_InputAxisValue > 0 && transform.position.y >= maxY)
        {
            freeLookCamera.m_YAxis.m_InputAxisValue = 0;
        }

        // 확대 제한
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            freeLookCamera.m_Lens.FieldOfView -= scroll * 10f;
            freeLookCamera.m_Lens.FieldOfView = Mathf.Clamp(freeLookCamera.m_Lens.FieldOfView, minZoom, maxZoom);
        }
    }
}
