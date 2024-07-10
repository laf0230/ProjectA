using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 2;
    public float scrollMin = 0f;
    public float scrollMax = 100f;

    private Vector3 dragOrigin;

    void Update()
    {
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

        if(Input.mouseScrollDelta.y != 0f)
        {
            Debug.Log("ScrollData: " + Input.mouseScrollDelta);
            
            float scrollVerify = Mathf.Clamp(scrollMin, scrollMax, dragSpeed);

        }
        
        /*
        Camera camera = Camera.main;
        Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        */
    }
}