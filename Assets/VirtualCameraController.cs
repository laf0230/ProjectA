using UnityEngine;
using Cinemachine;

public class VirtualCameraController : MonoBehaviour
{
    public float dragSpeed = 2; // �巡�� �ӵ�
    public float zoomSpeed = 2; // Ȯ�� �ӵ�
    public float minZoom = 5f; // �ּ� Ȯ�� �Ÿ�
    public float maxZoom = 15f; // �ִ� Ȯ�� �Ÿ�

    private Vector3 dragOrigin;
    private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // �巡�׷� ī�޶� �̵�
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(dragOrigin - Input.mousePosition);
        Vector3 move = ray.direction * dragSpeed * Time.deltaTime;

        transform.Translate(move, Space.World);

        // �ٷ� ī�޶� Ȯ��
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            virtualCamera.m_Lens.FieldOfView -= scroll * zoomSpeed;
            virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(virtualCamera.m_Lens.FieldOfView, minZoom, maxZoom);
        }
    }
}
