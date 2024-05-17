using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaceCharacter, ICauseDamage 
{
 
    [SerializeField] public float walkSpeed = 5;
    public float DamageAmount { get;} = 10f;
    public float attackCooldown = 2f;

    private GameObject target;
    private Vector3 moveDirection;
    private Gun gun;

    private Rigidbody enemyBody;
    private float nextTimeToShoot;

    void Start()
    {
        gun = GetComponent<Gun>();
        enemyBody = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Attack()
    {
        gun.Shoot(moveDirection);
    }

    void Update()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        moveDirection = direction;
        if (Time.time > nextTimeToShoot)
        {
            Attack();
            nextTimeToShoot = Time.time + attackCooldown;
        }
    }

    private void FixedUpdate()
    {
        enemyBody.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z) * walkSpeed;
    }
}
