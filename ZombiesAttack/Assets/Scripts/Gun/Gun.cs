using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float fireRate; // Скорострельность (выстрелов в секунду)
    public GameObject bulletPrefab;      // Спрайт пули
    [SerializeField] Transform bullets;
    private void Start()
    {
        fireRate = Parameters.fireRate/100;
        StartCoroutine(Shooting());
    }

    public void Shoot()
    {
        // Создаем пулю
        Instantiate(bulletPrefab, transform.position, transform.rotation, bullets);
    }

    private IEnumerator Shooting()
    {
        while (true) // Постоянно выполняем корутину
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    
}


