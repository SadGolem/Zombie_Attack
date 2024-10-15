using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float fireRate; // Скорострельность (выстрелов в секунду)
    public GameObject bulletPrefab;   
    [SerializeField] Transform bullets;
    private void Start()
    {
        fireRate = Parameters.fireRate/100;
        StartCoroutine(Shooting());
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation, bullets);
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    
}


