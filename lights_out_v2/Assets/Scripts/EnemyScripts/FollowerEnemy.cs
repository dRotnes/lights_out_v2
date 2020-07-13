using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : Enemy
{
    public float chasingDistance;
    public float stoppingDistance;
    public float attackingDistance;

    private Rigidbody2D _rb;
    private Transform _target;
    private Vector3 _startingPosition;
    private Vector2 _movement;


    private bool _canAttack;
    private bool _playerDead;

    private void Start()
    {
        _startingPosition = transform.position;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
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

        if (currentState == EnemyState.dead || _playerDead)
        {
            GetComponent<Rigidbody2D>().Sleep();
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 5f);
        }
        /*else
        {
            *//*if (_canAttack)
            {
                if (_timeBtwAttacks < -0)
                {
                    animator.SetTrigger("attack");
                    _timeBtwAttacks = startTimeBtwAttacks;
                }
                else
                {
                    _timeBtwAttacks -= Time.deltaTime;
                }
            }*//*
        }*/

    }
    private void FixedUpdate()
    {
        if (currentState != EnemyState.dead && !_playerDead)
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
        }
        else if (distance < stoppingDistance && distance > attackingDistance)
        {
            transform.position = this.transform.position;
            _movement = Vector2.zero;
            _canAttack = true;

        }
        else if (distance > chasingDistance)
        {
            _movement = (_startingPosition - transform.position).normalized;
            Vector3 temporaryMove = Vector2.MoveTowards(transform.position, _startingPosition, speed * Time.deltaTime);
            _rb.MovePosition(temporaryMove);
            _canAttack = false;
        }
    }

    /*public void Attack()
    {

        Collider2D[] hitArray = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D collider in hitArray)
        {
            GameObject player = collider.gameObject;
            knockback.Knock(player);
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);

        }
    }*/
}
