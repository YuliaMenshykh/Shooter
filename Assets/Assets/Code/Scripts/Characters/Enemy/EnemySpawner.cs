using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float minSpawnR = 10;
    float maxSpawnR = 20;

    public void SpawnEnemy()
    {
        GameObject enemy = EnemiesPool.SharedInstance.Get();
        if (enemy != null)
        {
            bool validPositionFound = false;
            Vector3 spawnPosition = Vector3.zero;
            int attempts = 0;
            const int maxAttempts = 50;

            while (!validPositionFound || attempts < maxAttempts)
            {
                attempts++;

                float randomAngle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
                float randomRadius = UnityEngine.Random.Range(minSpawnR, maxSpawnR);

                float offsetX = randomRadius * Mathf.Cos(randomAngle);
                float offsetZ = randomRadius * Mathf.Sin(randomAngle);

                float randomX = transform.position.x + offsetX;
                float randomZ = transform.position.z + offsetZ;

                spawnPosition = new Vector3(randomX, 1f, randomZ);

                // Check for overlap with other enemies
                Collider[] colliders = Physics.OverlapSphere(spawnPosition, enemy.GetComponent<Collider>().bounds.extents.magnitude);
                validPositionFound = colliders.Length == 0 || (colliders.Length == 1 && colliders[0].gameObject == enemy);
            }

            enemy.transform.position = spawnPosition;
            enemy.transform.rotation = Quaternion.identity;
            enemy.SetActive(true);
        }

    }
    
}
