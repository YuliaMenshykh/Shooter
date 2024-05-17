using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemiesPool : MonoBehaviour
{
    [SerializeField] public int initialAmountToPool = 5;
    public static EnemiesPool SharedInstance { get; private set; }
    public GameObject enemyToPool;

    private List<GameObject> pooledEnemies;
   
    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledEnemies = new List<GameObject>();
        CreateEnemy(initialAmountToPool);

    }

    // If all enemies are active - create a new one
    public GameObject Get()
    {
        foreach (var enemy in pooledEnemies)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }

        return CreateEnemy(1);
    }

    private GameObject CreateEnemy(int numToPool)
    {
        GameObject newEnemy = null;
        for (int i = 0; i < numToPool; i++)
        {
            newEnemy = Instantiate(enemyToPool);
            newEnemy.SetActive(false);
            pooledEnemies.Add(newEnemy);
        }
        return newEnemy;
    }

    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
    }

    public void KillClosestEnemy(Vector3 playerLocation)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach(GameObject target in pooledEnemies) 
        { 
            if (target.activeInHierarchy) 
            {
                Vector3 directionToTarget = playerLocation - target.transform.position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = target;
                }
            }
        }

        if (bestTarget != null)
        {
            ReturnToPool(bestTarget);
        }
        
    }

}

