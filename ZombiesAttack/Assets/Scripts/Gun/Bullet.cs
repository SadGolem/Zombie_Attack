using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public float speed = 10f;  // Скорость пули
    private int damageBullet;
    
    void Start()
    {
        damageBullet = Parameters.damageBullet;
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            other.GetComponent<Zombie>().TakeDamage(damageBullet);
            Destroy(gameObject);
        }
    }
}

