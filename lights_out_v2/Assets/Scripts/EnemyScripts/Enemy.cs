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
    public Knockback knock;
    public GameObject effect;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentState = EnemyState.idle;
        health = maxHealth.initialValue;
        knock = GetComponent<Knockback>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetMaxHealth();
    }
    public void TakeDamage(float damage, Collider2D other)
    {
        StartCoroutine(FlashDamage());
        knock.Knock(other, GetComponent<Rigidbody2D>());
        Instantiate(effect, transform.position, Quaternion.identity);
        health -= damage;
        UpdateHealth();
    }
    private IEnumerator FlashDamage()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
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
