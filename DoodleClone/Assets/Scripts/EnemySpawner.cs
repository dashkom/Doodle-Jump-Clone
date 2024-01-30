using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 10f;
    public float spawnHeightOffset = 2f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // �������� ��������� ������ ����������
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            Camera camera = Camera.main;
            float cameraWidth = camera.orthographicSize * camera.aspect;

            // ���������� ��������� ���������� ������ �������
            Vector3 spawnPosition = new Vector3(
                Random.Range(-cameraWidth, cameraWidth),
                camera.transform.position.y + spawnHeightOffset,
                transform.position.z
            );
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 1f);

            // ���������, ���� �� ����������� � ������� ������������
            bool spawnAllowed = true;
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    spawnAllowed = false;
                    break;
                }
            }

            // ���� ����������� ���, ������� ����������
            if (spawnAllowed)
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }

            // ������� ��������� �������� ����� ��������� �������
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }
}
