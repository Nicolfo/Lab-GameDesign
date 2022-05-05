using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public event System.Action onDeath;
    [SerializeField] float startingHealth;

    protected float health;
    protected bool dead;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // we will do some stuff for handling the hit variable
        TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0 && !dead) Die();
    }

    [ContextMenu("Self Destruct")]
    protected void Die()
    {
        dead = true;
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
