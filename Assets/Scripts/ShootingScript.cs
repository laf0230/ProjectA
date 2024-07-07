using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public float shootInterval = 2f; // 발사 간격
    public float shootSpeed = 10f; // 발사 속도
    public float bulletLifetime = 3f; // 총알 생존 시간

    void Start()
    {
        // 일정 간격으로 Shoot 함수 호출
        InvokeRepeating("Shoot", 0f, shootInterval);
    }

    void Shoot()
    {
        // 총알 생성
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // 카메라의 정면 방향으로 Raycast 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Raycast에 맞은 지점으로 이동하는 방향 계산
            Vector3 shootDirection = (hit.point - transform.position).normalized;

            // Rigidbody가 있는지 확인하고 발사
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = shootDirection * shootSpeed;
            }

            // 일정 시간이 지난 후에 총알 파괴
            Destroy(bullet, bulletLifetime);
        }
    }
}
