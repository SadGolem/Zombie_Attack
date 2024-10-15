using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 10f; // ���������������� (��������� � �������)
    public int speedShoot = 3;  // �������� ����
    public int damage = 1;      // ���� ����
    public Sprite bullet;      // ������ ����
    public Sprite shoot;        // ������ �������� (�� �������)

    private GameObject bulletObject; // ������ ����
    private float nextFireTime = 0f; // ����� ���������� ��������

    public void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            // ���������, ���� �� ��� ����
            if (bulletObject != null)
            {
                return; // ���� ����, �� ������� �����
            }

            // ������� ����
            bulletObject = new GameObject("Bullet");
            SpriteRenderer bulletRenderer = bulletObject.AddComponent<SpriteRenderer>();
            bulletRenderer.sprite = bullet;
            bulletRenderer.sortingLayerName = "Foreground";

            // ��������� ��������� ����
            bulletObject.transform.position = transform.position;

            // �������� ����
            StartCoroutine(MoveBullet());

            // ��������� ����� ���������� ��������
            nextFireTime = Time.time + (1f / fireRate);
        }
    }

    private IEnumerator MoveBullet()
    {
        while (true)
        {
            // ������� ���� ������
            bulletObject.transform.Translate(Vector2.right * speedShoot * Time.deltaTime);

            // ���������, �������� �� ���� ���� ������
            if (bulletObject.transform.position.x > Camera.main.orthographicSize * Screen.width / Screen.height)
            {
                // ������� ����
                Destroy(bulletObject);
                bulletObject = null;
                yield break; // ��������� �������� 
            }

            // ... (��� MoveBullet) 
            // ������ ����� while (true):

            // �������� �� ������������ � �����
            Collider2D[] hits = Physics2D.OverlapCircleAll(bulletObject.transform.position, 0.5f);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Zombie"))
                {
                    // ������� ���� �����
                    //hit.GetComponent<Zombie>().TakeDamage(damage);
                    Destroy(bulletObject); // ������� ����
                    bulletObject = null;
                    yield break; // ��������� ��������
                }
            }

            // ... (��������� ��� ��������)



            yield return null; // �������� ����������
        }
    }
}


