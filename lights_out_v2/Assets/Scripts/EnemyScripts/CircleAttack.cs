using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : MonoBehaviour
{
    private SpriteRenderer _sr;
    private float _chargeTime;
    private bool canDamage;
    private Collider2D colliderC;

    public float damage;
    public float startChargeTime;
    public float attackTime;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _chargeTime = startChargeTime;
        colliderC = GetComponent<Collider2D>();
        StartCoroutine(ChargeAttack());
    }
    private IEnumerator ChargeAttack()
    {
        while (_chargeTime > 0)
        {
            if (_sr.color.a > 0)
            {
                _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 0);
            }
            else
            {
                _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 1);
            }
            yield return new WaitForSeconds(_chargeTime);
            _chargeTime -= 0.02f;
        }
        Debug.Log("Attack is charged");
        _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 1);
        _chargeTime = startChargeTime;
        canDamage = true;
        colliderC.enabled = true;
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackTime);
        canDamage = false;
        FindObjectOfType<AudioManager>().Stop("CircleYellow");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canDamage)
        {
            Debug.Log("yessir");
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
