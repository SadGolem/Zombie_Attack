using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private ZombieData zombieData;
    private Transform playerTransform;
    private Rigidbody rb;
    private int health;

    private void Start()
    {
        health = zombieData.health;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Движение в сторону игрока
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * zombieData.speed;
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
