using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : Enemy
{
    public float startTimeBtwAttacks;
    private float _timeBtwAttacks;

    public float attackRange;
    public LayerMask playerLayer;
    public Transform attackPoint;

    public float chasingDistance;
    public float stoppingDistance;
    public float attackingDistance;

    private Rigidbody2D _rb;
    private Transform _target;
    private Vector3 _startingPosition;
    private Vector2 _movement;


    private bool _canAttack;
    private bool _playerDead;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        currentState = EnemyState.idle;
        _timeBtwAttacks = startTimeBtwAttacks;
        _startingPosition = transform.position;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (_target.GetComponent<PlayerMovement>().currentState == PlayerState.dead)
        {
            _playerDead = true;
        }

        if (health <= 0)
        {
            Die();
        }

        if (_canAttack && !_playerDead)
        {
            if (_timeBtwAttacks < -0)
            {
                animator.SetTrigger("Attack");
                currentState = EnemyState.attacking;
                _timeBtwAttacks = startTimeBtwAttacks;
            }
            else
            {
                _timeBtwAttacks -= Time.deltaTime;
            }
        }
        
        if (_target.position.y > transform.position.y)
        {
            _spriteRenderer.sortingLayerName = "ForeGround";
        }
        else
        {
            _spriteRenderer.sortingLayerName = "Enemy";
        }
    }
    private void FixedUpdate()
    {
        if (currentState != EnemyState.dead && currentState != EnemyState.attacking && !_playerDead)
        {
            CheckingDistance();
        }
    }

    private void LateUpdate()
    {
        if (_movement != Vector2.zero)
        {

            animator.SetFloat("Horizontal", _movement.x);
            animator.SetFloat("Vertical", _movement.y);
        }
        animator.SetFloat("Velocity", _movement.sqrMagnitude);

    }

    private void CheckingDistance()
    {
        
        float distance = Vector2.Distance(transform.position, _target.position);
        if (distance <= chasingDistance && distance > stoppingDistance)
        {
            _movement = (_target.position - transform.position).normalized;
            Vector3 temporaryMove = Vector2.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
            _rb.MovePosition(temporaryMove);
            _canAttack = false;
            currentState = EnemyState.chasing;
        }
        else if (distance < stoppingDistance && distance > attackingDistance)
        {
            transform.position = this.transform.position;
            _movement = Vector2.zero;
            _canAttack = true;
            currentState = EnemyState.idle;

        }
        else if (distance > chasingDistance)
        {
            _movement = (_startingPosition - transform.position).normalized;
            Vector3 temporaryMove = Vector2.MoveTowards(transform.position, _startingPosition, speed * Time.deltaTime);
            _rb.MovePosition(temporaryMove);
            _canAttack = false;
            currentState = EnemyState.returning;
        }

        if(transform.position == _startingPosition)
        {
            currentState = EnemyState.idle;
        }
    }

    public void Attack()
    {

        Collider2D[] hitArray = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        int counter = 0;
        foreach (Collider2D collider in hitArray)
        {
            if (collider.CompareTag("Player") && counter < 1)
            {
                /*Debug.Log(collider.name);*/
                /*GameObject player = collider.gameObject;*/
                /*knockback.Knock(player);*/
                collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage/2);
                counter += 1;
            }

        }
        currentState = EnemyState.idle;
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

      
    