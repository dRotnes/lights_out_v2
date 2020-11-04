using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EyeState
{
    idle,
    preparing,
    waiting,
    attacking,
    recharging
}

public class BossEye: MonoBehaviour
{
    public EyeState state;
    [Header("Signals")]
    public SignalSend damageTaken;
    public SignalSend inSpot;
    public SignalSend attack;
    public SignalSend finishedAttacking;

    [Space]
    public BossManager bossManag;
    public float damage;
    public float speed = 150;

    public PlayerMovement player;

    [Header("MinX, MaxX, MinY, MaxY")]
    public float[] bounds;
    [Space]
    private float _timeBtwBlink;
    private Vector2 _originalSpot;
    private Vector2 _nextSpot;
    private Rigidbody2D _rb;

    public Animator animator;

    private void Start()
    {
        bossManag.eyes.Add(this);
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("RandomSpot", Time.time, 2f);

        _originalSpot = transform.position;
        _timeBtwBlink = Random.Range(1, 3);
        state = EyeState.idle;
        bounds = new float[] { 11.82f, 21.18f, -8.79f, -3.63f };
    }
    private void LateUpdate()
    {
        if(state == EyeState.idle)
        {

            if (_timeBtwBlink <= 0)
            {
                animator.SetTrigger("Blink");
                _timeBtwBlink = Random.Range(1,5);
            }
            else
            {
                _timeBtwBlink -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case EyeState.idle:
                Movement();
                break;

            case EyeState.preparing:
                MoveToAttack();
                break;
        }
    }

    private void RandomSpot()
    {
        float x = Random.Range(bounds[0], bounds[1]);
        float y = Random.Range(bounds[2], bounds[3]);

        _nextSpot = new Vector2(x, y);
    }
    private void Movement()
    {
        Vector2 direction = (_nextSpot - _rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        _rb.AddForce(force);
    }

    public void Damage(PlayerHealth player)
    {
        state = EyeState.attacking;
        player.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        bossManag.TakeDamage(damage);
    }

    public void PrepareToAttack()
    {
        state = EyeState.preparing;   
    }
    private void MoveToAttack()
    {
        if ((Vector2)transform.position == _originalSpot)
        {
            inSpot.RaiseSignal();
            Debug.Log("Eh o bala");
            animator.SetTrigger("Close");
            state = EyeState.waiting;
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, _originalSpot, speed/80 * Time.deltaTime);
    }
    public void Idle()
    {
        animator.SetTrigger("Open");
        state = EyeState.idle;
    }
    public void StartAttacking()
    {
        
        attack.RaiseSignal();
    }

    public IEnumerator FallCO()
    {
        finishedAttacking.RaiseSignal();
        animator.SetTrigger("Finished");
        state = EyeState.recharging;
        yield return new WaitForSeconds(8f);
        animator.SetTrigger("Rise");
        state = EyeState.idle;
    }

    public void Fall()
    {
        StartCoroutine(FallCO());
    }

}
