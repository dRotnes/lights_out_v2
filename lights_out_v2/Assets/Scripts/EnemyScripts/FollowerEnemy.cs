using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class FollowerEnemy : Enemy
{
    public float startTimeBtwAttacks;
    private float _timeBtwAttacks;
    public float attackRange;
    public LayerMask playerLayer;
    public Transform attackPoint;
    private int counter;

    private bool _canSeek = true;
    public Transform target;
    public GameObject player;
    public float speed;
    public float nextWaipointDistance;
    public float chaseRadius;

    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEnd;
    private bool _canChase;
    public GameObject startingPosition;

    private Seeker _seeker;
    private Rigidbody2D _rb;

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        transform.position = startingPosition.transform.position;
        target = startingPosition.transform;
        InvokeRepeating("UpdatePath", 0f, .5f);
        currentState = EnemyState.idle;
        

    }

    void UpdatePath()
    {
        if (_seeker.IsDone() && currentState != EnemyState.dead && _canSeek)
        {
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
        }
    }
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }
    private void Update()
    {
        
        if (_reachedEnd && target == player.transform)
        {
            currentState = EnemyState.attacking;
            if (_timeBtwAttacks < 0)
            {
                animator.SetTrigger("Attack");
                _timeBtwAttacks = startTimeBtwAttacks;
            }
            else
            {
                _timeBtwAttacks -= Time.deltaTime;
            }
        }
        
        if (player.transform.position.y > transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "ForeGround";
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
        }
        
    }
    private void FixedUpdate()
    {
        if (_path == null)
        {
            return;
        }
        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            _reachedEnd = true;
            
            return;
        }
        else
        {
            _reachedEnd = false;
        }
        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        _rb.AddForce(force);

        float playerDistance = Vector2.Distance(player.transform.position, transform.position);
        if(playerDistance <= chaseRadius && playerDistance > nextWaipointDistance)
        {
            _rb.isKinematic = false;
            target = player.transform;
            currentState = EnemyState.chasing;
        }
        else if(playerDistance <= nextWaipointDistance)
        {
            _rb.isKinematic = true;
            _rb.velocity = Vector2.zero;
        }
        else
        {
            _rb.isKinematic = false;
            target = startingPosition.transform;
            currentState = EnemyState.returning;
        }
        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
        if (distance < nextWaipointDistance)
        {
            _currentWaypoint++;
            
        }

        if(Vector2.Distance(transform.position,startingPosition.transform.position) <= 0.2f)
        {
            currentState = EnemyState.idle;
        }
    }

    private void LateUpdate()
    {
        if (_rb.velocity != Vector2.zero)
        {

            animator.SetFloat("Horizontal", _rb.velocity.x);
            animator.SetFloat("Vertical", _rb.velocity.x);
        }
        animator.SetFloat("Velocity", _rb.velocity.sqrMagnitude);

    }

    public void Attack()
    {

        Collider2D[] hitArray = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D collider in hitArray)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log(collider.name);
                GameObject player = collider.gameObject;
                collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage / 8);
                counter += 1;
            }
        }
    }
}


   /*

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
        if (currentState != EnemyState.chasing || currentState != EnemyState.returning)
        {
            _rb.Sleep();
        }
        else
        {
            _rb.WakeUp();
        }
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
        else
        {
            currentState = EnemyState.idle;
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

        if (transform.position == _startingPosition)
        {
            currentState = EnemyState.idle;
        }
    }

    


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }*/
/*}*/


    