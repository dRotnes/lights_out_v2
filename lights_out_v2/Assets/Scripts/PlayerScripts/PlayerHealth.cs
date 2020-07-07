using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SignalSend playerHealthSignal;
    public FloatValue currentHealth;
    public Collider2D footCollider;

    private bool _isInvencible;
    private Animator _animator;

    public void TakeDamage(float damage, bool invencibility = false)
    {

        if (_isInvencible == false)
        {
            /*FindObjectOfType<AudioManager>().Play("hit_sound");*/
            currentHealth.RuntimeValue -= damage;
            playerHealthSignal.RaiseSignal();
            _animator.SetTrigger("hit");
            if(invencibility == true)
            {
                StartCoroutine(Invencible());
            }

        }
    }

    private void Die()
    {
        
        _animator.SetTrigger("die");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().Sleep();
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
