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
    [Header("Enemy General")]
    [Space]
    public float health;
    public float attackDamage;
    public bool canKnock;
    public bool playerInvencible;
    public EnemyState currentState;
    public Animator animator;
    public FloatValue maxHealth;
    public GameObject destroyable;
    public GameObject enemyCanvas;
    public Slider healthSlider;
    public Gradient gradient;
    public Image fill;
    public Knockback knock;
    public GameObject bloodEffect;
    public SpriteRenderer spriteRenderer;
    public GameObject soul;
    public GameObject soulEffect;
    public GameObject heart;
    public bool isDead = false;
    public SavingManager sm;

    private void Awake()
    {
        sm = FindObjectOfType<SavingManager>();
        sm.AddToArray(null, null, null, this);
        currentState = EnemyState.idle;
        health = maxHealth.value;
        knock = GetComponent<Knockback>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetMaxHealth();
    }
    public void TakeDamage(float damage, Collider2D other = null)
    {
        StartCoroutine(FlashDamage());
        if (canKnock)
        {
            knock.Knock(other, GetComponent<Rigidbody2D>());
        }
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        CinemachineShake.Instance.ShakeCam(1f, .1f);
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
        isDead = true;
        int[] randomArray = new int[] { 1, 0, 1, 0, 1, 1 };
        int randomNumber = Random.Range(0, 5);
        if (randomArray[randomNumber] == 0)
            Instantiate(heart, transform.position, Quaternion.identity);
        healthSlider.gameObject.SetActive(false);
        currentState = EnemyState.dead;
        animator.SetBool("Dead", true);
        StartCoroutine(InstantiateSoul());
    }
    private IEnumerator InstantiateSoul()
    {
        yield return new WaitForSeconds(1.9f);
        Instantiate(soulEffect, transform.position, Quaternion.identity);
        Instantiate(soul, transform.position, Quaternion.identity);
        destroyable.SetActive(false);
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

    public void SetStatus(bool value)
    {
        isDead = value;
    }
    public bool GetStatus()
    {
        return isDead;
    }
}
