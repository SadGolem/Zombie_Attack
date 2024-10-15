using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnZombie : MonoBehaviour
{
    [SerializeField] private List<GameObject> zombiePrefabs;
    [SerializeField] private Transform spawnArea; // Transform ������� ������
    [SerializeField] private Transform spawnTransform; // ��� ���������� ������� � ��������� Unity (�� �����)

    private int chanceSpawnZombieArmored;
    private int chanceSpawnZombieVeteran;
    private float spawnInterval = 2.1f; // ��������� �������� ������
    private float minSpawnInterval = 0.5f; // ����������� �������� ������

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
            // ������� �����
            Spawn();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator SpawnIntervalChange()
    {
        while (true)
        {
            // ��������� ����� ������ ������ 10 ������
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
        // �������� ��������� ������ �����
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

        // ���������� ��������� ����� ������ �� ��������� ������
        Vector3 spawnPosition = GenerateSpawnPosition();

        // ������� �����
        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity, spawnTransform);
    }

    private Vector3 GenerateSpawnPosition()
    {
        // ���������� ������� spawnArea
        Vector3 spawnAreaSize = spawnArea.localScale;

        // ���������� ��������� ���������� x � y ������ spawnArea, �� �� ��������� ������
        float x, y, z;

        x = Random.Range(spawnArea.position.x - spawnAreaSize.x, spawnArea.position.x + spawnAreaSize.x / 5);
        z = Random.Range(spawnArea.position.z - spawnAreaSize.y, spawnArea.position.z + spawnAreaSize.y / 5);
        y = 1.03f;
        return new Vector3(x, y, z);
    }
}

