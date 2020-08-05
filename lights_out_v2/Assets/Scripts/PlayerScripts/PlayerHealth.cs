using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SignalSend playerHealthSignal;
    public FloatValue currentHealth;
    public Collider2D footCollider;

    private PlayerMovement _playerMovement;
    private bool _isInvencible;
    private Animator _animator;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (currentHealth.RuntimeValue <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float damage, bool invencibility = false)
    {
        
        if (_isInvencible == false)
        {
            StartCoroutine(FlashDamage());
            /*FindObjectOfType<AudioManager>().Play("hit_sound");*/
            CinemachineShake.Instance.ShakeCam(1f, .1f);
            currentHealth.RuntimeValue -= damage;
            playerHealthSignal.RaiseSignal();
            if (invencibility == true)
            {
                StartCoroutine(Invencible());
            }

        }

    }

    private IEnumerator FlashDamage()
    {
        _sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _sr.color = Color.white;
    }

    private void Die()
    {
        _playerMovement.currentState = PlayerState.dead;
        _animator.SetBool("Dead", true);
        footCollider.enabled = false;
        
    }

    public void Fall()
    {
        _animator.SetTrigger("fall");

    }

    private IEnumerator Invencible()
    {
        _isInvencible = true;
        yield return new WaitForSeconds(1f);
        _isInvencible = false;
    }


}
