using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnZombie : MonoBehaviour
{
    [SerializeField] private List<GameObject> zombiePrefabs;
    [SerializeField] private Transform spawnArea; // Transform объекта спавна
    [SerializeField] private Transform spawnTransform; // Для сохранения чистоты в редакторе Unity (на сцене)

    private int chanceSpawnZombieArmored;
    private int chanceSpawnZombieVeteran;
    private float spawnInterval = 2.1f; // Начальный интервал спавна
    private float minSpawnInterval = 0.5f; // Минимальный интервал спавна

    private void Start()
    {
        chanceSpawnZombieArmored = Parameters.chanceSpawnZombieArmored;
        chanceSpawnZombieVeteran = Parameters.chanceSpawnZombieVeteran;
        StartCoroutine(SpawnZombies());
        StartCoroutine(SpawnIntervalChange());
    }

    private IEnumerator SpawnZombies()
    {
        while (true)
        {
            // Спавним зомби
            Spawn();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator SpawnIntervalChange()
    {
        while (true)
        {
            // Уменьшаем время спавна каждые 10 секунд
            Debug.Log(spawnInterval);
            if (spawnInterval != minSpawnInterval)
            {
                spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - 0.1f);
            }
            if (spawnInterval == minSpawnInterval)
                yield break;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Spawn()
    {
        // Выбираем случайный префаб зомби
        int randomIndex = Random.Range(0, 99);
        GameObject zombiePrefab;
        if (randomIndex <= chanceSpawnZombieArmored)
        {
            zombiePrefab = zombiePrefabs[0];
        }
        else if (randomIndex <= chanceSpawnZombieVeteran)
        {
            zombiePrefab = zombiePrefabs[1];
        }
        else
        {
            zombiePrefab = zombiePrefabs[2];
        }

        // Генерируем случайную точку спавна за пределами камеры
        Vector3 spawnPosition = GenerateSpawnPosition();

        // Спавним зомби
        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity, spawnTransform);
    }

    private Vector3 GenerateSpawnPosition()
    {
        // Определяем границы spawnArea
        Vector3 spawnAreaSize = spawnArea.localScale;

        // Генерируем случайные координаты x и y внутри spawnArea, но за пределами камеры
        float x, y, z;

        x = Random.Range(spawnArea.position.x - spawnAreaSize.x, spawnArea.position.x + spawnAreaSize.x / 5);
        z = Random.Range(spawnArea.position.z - spawnAreaSize.y, spawnArea.position.z + spawnAreaSize.y / 5);
        y = 1.03f;
        return new Vector3(x, y, z);
    }
}

