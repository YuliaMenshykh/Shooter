using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeComponent : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 100f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0f;
    }

    public void ReceiveDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
    }

    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }
}
