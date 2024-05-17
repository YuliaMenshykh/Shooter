using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [SerializeField] private float velocity = 1400f;
    [SerializeField] private Bullet projectilePrefab;
    [SerializeField] private Transform startPoint;
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;
    private IObjectPool<Bullet> objectPool;

    // exception if item already in the pool
    private bool collectionCheck = true;


    private void Awake()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet,
            OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
    }

    private Bullet CreateBullet()
    {
        Bullet projectileInstance = Instantiate(projectilePrefab);
        projectileInstance.ObjectPool = objectPool;
        return projectileInstance;
    }

    private void OnGetFromPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    // when exceed the maximum number of pooled items
    private void OnDestroyPooledObject(Bullet pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    public void Shoot(Vector3 direction)
    {
        if (objectPool != null)
        {
            Bullet bulletObject = objectPool.Get();

            if (bulletObject == null)
                return;

            AlineToStart(bulletObject);

            MoveBullet(direction, bulletObject);

            bulletObject.Deactivate();
        }
    }

    private void MoveBullet(Vector3 direction, Bullet bulletObject)
    {
        bulletObject.GetComponent<Rigidbody>().AddForce(direction * velocity, ForceMode.Acceleration);
    }

    private void AlineToStart(Bullet bulletObject)
    {
        bulletObject.transform.SetPositionAndRotation(startPoint.transform.position, startPoint.transform.rotation);
    }
}

