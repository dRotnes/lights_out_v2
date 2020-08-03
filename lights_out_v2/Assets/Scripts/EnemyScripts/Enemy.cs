using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
    public float attackDamage;

    public EnemyState currentState;
    public Animator animator;
    public FloatValue maxHealth;
    public GameObject destroyable;
    public GameObject enemyCanvas;
    public Slider healthSlider;
    public Gradient gradient;
    public Image fill;

    private void Awake()
    {
        currentState = EnemyState.idle;
        health = maxHealth.initialValue;
        SetMaxHealth();
    }
    public void TakeDamage(float damage)
    {
        /*animator.SetTrigger("Hit");*/
        health -= damage;
        UpdateHealth();
    }
    public void Die()
    {
        healthSlider.gameObject.SetActive(false);
        currentState = EnemyState.dead;
        animator.SetBool("Dead", true);
        Destroy(destroyable, 5f);
    }
    public void SetMaxHealth()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;

        fill.color = gradient.Evaluate(1f);
    }
    public void UpdateHealth()
    {
        healthSlider.value = health;
        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
        
    }
}
