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

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            TakeDamage(1);
        }
    }
    public void TakeDamage(float damage, bool invencibility = false)
    {
        
       
        if (_isInvencible == false)
        {
            /*FindObjectOfType<AudioManager>().Play("hit_sound");*/
            currentHealth.RuntimeValue -= damage;
            playerHealthSignal.RaiseSignal();
            /* _animator.SetTrigger("hit");*/
            if (invencibility == true)
            {
                StartCoroutine(Invencible());
            }

        }

        if (currentHealth.RuntimeValue <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        _animator.SetTrigger("Die");
        _playerMovement.currentState = PlayerState.dead;
        footCollider.enabled = false;
        this.enabled = false;
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
