using UnityEngine;
using Cinemachine;

public class VirtualCameraController : MonoBehaviour
{
    public float dragSpeed = 2; // 드래그 속도
    public float zoomSpeed = 2; // 확대 속도
    public float minZoom = 5f; // 최소 확대 거리
    public float maxZoom = 15f; // 최대 확대 거리

    private Vector3 dragOrigin;
    private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // 드래그로 카메라 이동
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(dragOrigin - Input.mousePosition);
        Vector3 move = ray.direction * dragSpeed * Time.deltaTime;

        transform.Translate(move, Space.World);

        // 휠로 카메라 확대
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            virtualCamera.m_Lens.FieldOfView -= scroll * zoomSpeed;
            virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(virtualCamera.m_Lens.FieldOfView, minZoom, maxZoom);
        }
    }
}
