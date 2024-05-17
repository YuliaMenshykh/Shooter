using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, ICauseDamage
{
    // Reference to its ObjectPool
    public IObjectPool<Bullet> ObjectPool { set => objectPool = value; }
    [SerializeField] public float DamageAmount { get;} = 10f;


    [SerializeField] private float lifetimeDelay = 0.5f;
    private IObjectPool<Bullet> objectPool;
    private bool isReleased;

    // Handle bullets deactivation
    private void OnEnable()
    {
        isReleased = false;
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(lifetimeDelay));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        if (!isReleased)
        {
            yield return new WaitForSeconds(delay);

            ResetRigidbody();
            objectPool.Release(this);
        }
    }

    private void ResetRigidbody()
    {
        Rigidbody rBody = GetComponent<Rigidbody>();
        rBody.velocity = new Vector3(0f, 0f, 0f);
        rBody.angularVelocity = new Vector3(0f, 0f, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isReleased)
        {
            if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Floor"))
            {
                ResetRigidbody();
                objectPool.Release(this);
                isReleased = true;
            }
          
        }

    }

    
}
