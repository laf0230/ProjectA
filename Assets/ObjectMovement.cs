using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도 조절 변수
    public float jumpForce = 10f; // 점프 힘 조절 변수

    void Update()
    {
        // 키보드 입력 받기
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 이동 방향 계산
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // 현재 위치에 이동 방향을 더하기
        transform.Translate(movement);

        // 점프
        if (Input.GetButtonDown("Jump"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
