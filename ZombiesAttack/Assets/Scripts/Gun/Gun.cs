using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 10f; // Скорострельность (выстрелов в секунду)
    public int speedShoot = 3;  // Скорость пули
    public int damage = 1;      // Урон пули
    public Sprite bullet;      // Спрайт пули
    public Sprite shoot;        // Спрайт выстрела (по желанию)

    private GameObject bulletObject; // Объект пули
    private float nextFireTime = 0f; // Время следующего выстрела

    public void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            // Проверяем, есть ли уже пуля
            if (bulletObject != null)
            {
                return; // Если есть, не создаем новую
            }

            // Создаем пулю
            bulletObject = new GameObject("Bullet");
            SpriteRenderer bulletRenderer = bulletObject.AddComponent<SpriteRenderer>();
            bulletRenderer.sprite = bullet;
            bulletRenderer.sortingLayerName = "Foreground";

            // Начальное положение пули
            bulletObject.transform.position = transform.position;

            // Движение пули
            StartCoroutine(MoveBullet());

            // Обновляем время следующего выстрела
            nextFireTime = Time.time + (1f / fireRate);
        }
    }

    private IEnumerator MoveBullet()
    {
        while (true)
        {
            // Двигаем пулю вперед
            bulletObject.transform.Translate(Vector2.right * speedShoot * Time.deltaTime);

            // Проверяем, достигла ли пуля края экрана
            if (bulletObject.transform.position.x > Camera.main.orthographicSize * Screen.width / Screen.height)
            {
                // Удаляем пулю
                Destroy(bulletObject);
                bulletObject = null;
                yield break; // Прерываем итерацию 
            }

            // ... (код MoveBullet) 
            // Внутри цикла while (true):

            // Проверка на столкновение с зомби
            Collider2D[] hits = Physics2D.OverlapCircleAll(bulletObject.transform.position, 0.5f);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Zombie"))
                {
                    // Нанести урон зомби
                    //hit.GetComponent<Zombie>().TakeDamage(damage);
                    Destroy(bulletObject); // Удалить пулю
                    bulletObject = null;
                    yield break; // Прерываем корутину
                }
            }

            // ... (остальной код корутины)



            yield return null; // Передача управления
        }
    }
}


