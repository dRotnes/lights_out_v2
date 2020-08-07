using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged Enemy")]
    [Space]
    public Transform rangePoint;
    public float range;
    public LayerMask playerLayer;
    public GameObject attackCircle;
    public float startChargeTime;
    public float resetTime;
    public float damageRate;
    public float attackTime;
    public GameObject enemyCollider;
    
    private float _chargeTime;
    private bool _canAttack = true;
    private bool _playerInRange;

    private void Start()
    {
        _chargeTime = startChargeTime;
    }
    private void Update()
    {
        if (health <= 0)
        {
            Die();
            Destroy(enemyCollider);
        }
        if(currentState == EnemyState.dead)
        {
            this.enabled = false;
        }
        if (_playerInRange)
        {
            enemyCanvas.SetActive(true);
            switch(_canAttack)
            {
                case true:
                    StartAttack();
                    break;
                case false:
                    return;
            }
        }

    }
    public void StartAttack()
    {
        currentState = EnemyState.attacking;
        Debug.Log("Attack starting");
        _canAttack = false;
        animator.SetTrigger("StartAttack");
    }

    public IEnumerator ChargeAttack()
    {
        attackCircle.SetActive(true);
        SpriteRenderer sr = attackCircle.GetComponent<SpriteRenderer>();
        while (_chargeTime > 0)
        {
            if (sr.color.a > 0)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
            }
            else
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            }
            yield return new WaitForSeconds(_chargeTime);
            _chargeTime -= 0.01f;
        }
        Debug.Log("Attack is charged");
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        _chargeTime = startChargeTime;
        animator.SetBool("Attack", true);
    }

    private void Damage()
    {
        Collider2D[] hitArray = Physics2D.OverlapCircleAll(rangePoint.position, range, playerLayer);
        foreach (Collider2D collider in hitArray)
        {
            if (collider.CompareTag("Player") && currentState != EnemyState.dead)
            {
                GameObject player = collider.gameObject;
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage, playerInvencible);
            }
        } 
    }
    public IEnumerator Attack()
    {
        Debug.Log("Attacking");
        InvokeRepeating("Damage", 0f, damageRate);
        yield return new WaitForSeconds(attackTime);
        Debug.Log("Finished Attack");
        CancelInvoke("Damage");
        animator.SetBool("Attack", false);
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        Debug.Log("Reseting Attack");
        yield return new WaitForSeconds(resetTime);
        Debug.Log("Can attack again");
        currentState = EnemyState.idle;
        _canAttack = true;
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (rangePoint.position == null)
            return;
        Gizmos.DrawWireSphere(rangePoint.position, range);
    }

}
