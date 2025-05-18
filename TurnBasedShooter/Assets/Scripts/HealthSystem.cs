using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDead;
    public event EventHandler OnDamage;

    [SerializeField]private int health = 100;
    private int healthMax;

    private void Awake()
    {
        healthMax = health;
    }

    public float GetNormalizedHealth()
    {
        return (float)health/healthMax; //setup as int so this helps
    }

    public void Damage(int damageAmt)
    {
        health -= damageAmt;

        if (health < 0)
        {
            health = 0;
        }

        OnDamage?.Invoke(this, EventArgs.Empty);

        if (health == 0) 
        {
            Die();
        }

        Debug.Log(health);
    }

    private void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty); //invoking this event when health less than 0
    }
}
