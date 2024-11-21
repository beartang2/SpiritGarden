using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] BoxCollider spawnRange; // ���� ����

    [SerializeField] private float spawnInterval = 2.0f;

    private float spawnTimer = 0f;

    private void Start()
    {
        foreach (var enemy in enemies)
        {
            enemy.SetActive(false);
            enemy.transform.position = Vector3.zero;
        }

        Vector3 spawnPoint = GetRandomPosition();
        Debug.Log("Random Pos" + spawnPoint);
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        SpawnEnemy();
    }

    private Vector3 GetRandomPosition()
    {
        Bounds bounds = spawnRange.bounds;

        float randomX = Random.Range(-spawnRange.size.x / 2, spawnRange.size.x / 2);
        float randomZ = Random.Range(-spawnRange.size.z / 2, spawnRange.size.z / 2);

        return new Vector3(bounds.center.x + randomX, bounds.center.y, bounds.center.z + randomZ);
    }

    public void SpawnEnemy()
    {
        // spawnInterval�� ������ �� ����
        if (spawnTimer >= spawnInterval)
        {
            Vector3 spawnPosition = GetRandomPosition();

            foreach (var enemy in enemies)
            {
                if (!enemy.activeInHierarchy) // ��Ȱ��ȭ�� ���� Ȱ��ȭ
                {
                    enemy.SetActive(true);
                    enemy.transform.position = spawnPosition;
                    break; // �� ���� Ȱ��ȭ�ϰ� Ż��
                }
            }

            spawnTimer = 0f; // Ÿ�̸� �ʱ�ȭ
        }
    }
}
