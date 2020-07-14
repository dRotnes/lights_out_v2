using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    chasing, 
    attacking, 
    returning,
    dead
}
public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public float attackDamage;

    public EnemyState currentState;
    public Animator animator;
    public FloatValue maxHealth;

    private void Awake()
    {
        currentState = EnemyState.idle;
        health = maxHealth.initialValue;
    }
    private void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float damage)
    {
        /*animator.SetTrigger("Hit");*/
        health -= damage;
    }
    public void Die()
    {
        currentState = EnemyState.dead;
        animator.SetBool("Dead", true);
    }
}
