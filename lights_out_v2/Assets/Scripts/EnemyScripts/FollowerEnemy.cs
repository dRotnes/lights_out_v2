using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Pathfinding.RVO;
public class FollowerEnemy : Enemy
{
    public float startTimeBtwAttacks;
    public float attackRange;
    public LayerMask playerLayer;
    public Transform attackPoint;

    public Transform target;
    public GameObject player;
    public float speed;
    public float nextWaipointDistance;
    public float chaseRadius;

    private Path _path;
    private int _currentWaypoint = 0;
    public GameObject startingPosition;

    private float _timeBtwAttacks;
    private int counter = 0;
    private bool _reachedEnd;
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
        if (_seeker.IsDone() && currentState != EnemyState.dead)
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
        if (health <= 0)
        {
            Die();
        }
        if (currentState != EnemyState.dead)
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
        else
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
            this.enabled = false;
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
            _rb.WakeUp();
            target = player.transform;
            currentState = EnemyState.chasing;
            enemyCanvas.SetActive(true);
            counter = 0;
        }
        else if(playerDistance <= nextWaipointDistance)
        {
            _rb.Sleep();
            _rb.velocity = Vector2.zero;
            if(counter == 0)
            {
                AstarPath.active.Scan();
                counter = 1;
            }
            
        }
        else
        {
            _rb.WakeUp();
            target = startingPosition.transform;
            currentState = EnemyState.returning;
            counter = 0;
            enemyCanvas.SetActive(false);
        }
        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
        if (distance < nextWaipointDistance)
        {
            _currentWaypoint++;
            
        }

        if(Vector2.Distance(transform.position,startingPosition.transform.position) <= 0.1f)
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
                GameObject player = collider.gameObject;
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage, playerInvencible);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (transform.position == null)
            return;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}

