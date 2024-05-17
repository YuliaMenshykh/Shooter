using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : BaceCharacter
{
    public UserInterface userInterface;
    public AttributeComponent healthAttribute;
    
    void Start()
    {
        healthAttribute = GetComponent<AttributeComponent>();
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            userInterface = canvas.GetComponent<UserInterface>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ICauseDamage damageDealer = collision.gameObject.GetComponent<ICauseDamage>();
        if (damageDealer != null)
        {
            HandleDamage(damageDealer.DamageAmount);
        }
    }

    void HandleDamage(float damage)
    {
        if (healthAttribute.IsAlive())
        {
            healthAttribute.ReceiveDamage(damage);
            UpdateHealthSlider();
        }
        

    }

    private void UpdateHealthSlider()
    {
        if (userInterface != null) 
        {
            userInterface.SetHealthBarPercent(healthAttribute.GetHealthPercent());
        }
        
    }
    public override void Attack()
    {
        EnemiesPool.SharedInstance.KillClosestEnemy(transform.position);
    }

    void Update()
    {
        if (healthAttribute.IsAlive())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }
        else
        {
            userInterface.GameOverScreen();
            Time.timeScale = 0;
        }
    }

}
