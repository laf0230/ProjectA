using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public float shootInterval = 2f; // �߻� ����
    public float shootSpeed = 10f; // �߻� �ӵ�
    public float bulletLifetime = 3f; // �Ѿ� ���� �ð�

    void Start()
    {
        // ���� �������� Shoot �Լ� ȣ��
        InvokeRepeating("Shoot", 0f, shootInterval);
    }

    void Shoot()
    {
        // �Ѿ� ����
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // ī�޶��� ���� �������� Raycast ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Raycast�� ���� �������� �̵��ϴ� ���� ���
            Vector3 shootDirection = (hit.point - transform.position).normalized;

            // Rigidbody�� �ִ��� Ȯ���ϰ� �߻�
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = shootDirection * shootSpeed;
            }

            // ���� �ð��� ���� �Ŀ� �Ѿ� �ı�
            Destroy(bullet, bulletLifetime);
        }
    }
}
