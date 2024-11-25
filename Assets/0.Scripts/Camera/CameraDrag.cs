using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 2;
    public float scrollMin = 0f;
    public float scrollMax = 100f;
    public NavMeshAgent agent;
    public float checkRadius = 0.5f;

    private Vector3 dragOrigin;
    private Vector3 lastValidPosition;

    void Update()
    {
        if(NavMesh.SamplePosition(transform.position, out NavMeshHit hit, checkRadius, NavMesh.AllAreas))
        {
            lastValidPosition = hit.position;
        }
        else
        {
            transform.position = lastValidPosition;
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

            transform.Translate(move, Space.World);
        }

        if (Input.mouseScrollDelta.y != 0f)
        {
            // Debug.Log("ScrollData: " + Input.mouseScrollDelta);

            float scrollVerify = Mathf.Clamp(scrollMin, scrollMax, dragSpeed);
        }
    }

    public void SetFocus(GameObject character)
    {
        // ī�޶��� ĳ���� ��Ŀ��
        if (!character.activeSelf)
        {
            transform.position = character.transform.position;
        }
    }
}