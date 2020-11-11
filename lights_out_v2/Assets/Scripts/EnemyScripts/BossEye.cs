using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EyeState
{
    idle,
    preparing,
    waiting,
    attacking,
    recharging,
    dead
}

public class BossEye : MonoBehaviour
{
    public EyeState state;
    [Header("Signals")]
    public SignalSend damageTaken;
    public SignalSend inSpot;
    public SignalSend attack;
    public SignalSend finishedAttacking;
    public SignalSend finishedAll;

    [Space]
    public BossManager bossManag;
    public float damage;
    public float speed = 150;

    public PlayerMovement player;
    public Transform playerPos;
    public GameObject bloodEffect;


    [Header("MinX, MaxX, MinY, MaxY")]
    public float[] bounds;
    [Space]
    private float _timeBtwBlink;
    private Vector2 _originalSpot;
    private Vector2 _nextSpot;
    private Rigidbody2D _rb;
    private Collider2D _co;
    private SpriteRenderer _sr;

    public Animator animator;

    private void Start()
    {
        bossManag.eyes.Add(this);
        _rb = GetComponent<Rigidbody2D>();
        _co = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        InvokeRepeating("RandomSpot", Time.time, 2f);

        _originalSpot = transform.position;
        _timeBtwBlink = Random.Range(1, 3);
        state = EyeState.idle;
        bounds = new float[] { 11.74f, 20.98f, -8.87f, -3.8f };
    }
    private void LateUpdate()
    {
        if (state == EyeState.idle)
        {

            if (_timeBtwBlink <= 0)
            {
                animator.SetTrigger("Blink");
                _timeBtwBlink = Random.Range(1, 5);
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

            case EyeState.dead:
                Destroy(gameObject);
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

    public void TakeDamage(float damage)
    {
        FindObjectOfType<AudioManager>().Play("HitSound");
        StartCoroutine(FlashDamage());
        bossManag.TakeDamage(damage);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        CinemachineShake.Instance.ShakeCam(1f, .2f);
    }
    private IEnumerator FlashDamage()
    {
        _sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _sr.color = Color.white;
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
        transform.position = Vector2.MoveTowards(transform.position, _originalSpot, speed / 80 * Time.deltaTime);
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
        _rb.isKinematic = true;
        _co.enabled = true;
        finishedAttacking.RaiseSignal();
        animator.SetTrigger("Finished");
        state = EyeState.recharging;
        yield return new WaitForSeconds(7f);
        _rb.isKinematic = false;
        _co.enabled = false;
        animator.SetTrigger("Rise");
        state = EyeState.idle;
        finishedAll.RaiseSignal();
    }

    public void Fall()
    {
        StartCoroutine(FallCO());
    }

    public void Dead()
    {
        state = EyeState.dead;
    }


}
