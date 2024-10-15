using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private ZombieData zombieData;
    [SerializeField] private Animator animator;
    private Transform playerTransform;
    private Rigidbody rb;
    private int health;

    private void Start()
    {
        health = zombieData.health;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        if (animator != null)
        {
            animator.Play("Z_Run_InPlace");
        }
        
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // �������� � ������� ������
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // ����������� �������
        rb.MovePosition(transform.position + direction * zombieData.speed * Time.deltaTime * 2);

        // �������� � ������� ��������
        if (direction != Vector3.zero)
        {
            // ��������� ���� ��������
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            // ������� �������
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Parameters.points += zombieData.points;
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }


}
