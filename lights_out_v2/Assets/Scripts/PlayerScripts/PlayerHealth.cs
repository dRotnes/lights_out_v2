using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SignalSend playerHealthSignal;
    public FloatValue currentHealth;
    public Collider2D footCollider;
    public float startTimeInvencible;
    public Material _blinkMat;

    private float _timeInvencible;
    private Material _mainMat;
    private PlayerMovement _playerMovement;
    private bool _isInvencible;
    private Animator _animator;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _timeInvencible = startTimeInvencible;
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _mainMat = GetComponent<SpriteRenderer>().material;
    }
    private void Update()
    {
        if (currentHealth.RuntimeValue <= 0)
        {
            _isInvencible = false;
            StopAllCoroutines();
            Die();
            
        }
        if (_isInvencible)
        {
            StartCoroutine(FlashInvencible());
            if (_timeInvencible < 0)
            {
                Debug.Log("Can damage again");
                StopAllCoroutines();
                _sr.material = _mainMat;
                _isInvencible = false;
                _timeInvencible = startTimeInvencible;
            }
            else
            {
                _timeInvencible -= Time.deltaTime;
            }
        }
    }
    public void TakeDamage(float damage, bool enemy = false)
    {
        Debug.Log(_isInvencible);
        if(_isInvencible == false)
        {
            currentHealth.RuntimeValue -= damage;
            if (enemy)
            {
                _isInvencible = true;
            }
            else
            {

                StartCoroutine(FlashDamage());
            }
            /*FindObjectOfType<AudioManager>().Play("hit_sound");*/
            CinemachineShake.Instance.ShakeCam(1f, .1f);
            playerHealthSignal.RaiseSignal();
        }
        

    }

    private IEnumerator FlashDamage()
    {
        _sr.material = _blinkMat;
        yield return new WaitForSeconds(.1f);
        _sr.material = _mainMat;
    }
    private IEnumerator FlashInvencible() 
    {
        _sr.material = _blinkMat;
        yield return new WaitForSeconds(.05f);
        _sr.material = _mainMat;
        yield return new WaitForSeconds(.05f);
        StartCoroutine(FlashInvencible());
    }

    private void Die()
    { 
        _sr.material = _mainMat;
        _playerMovement.currentState = PlayerState.dead;
        _animator.SetBool("Dead", true);
        footCollider.enabled = false;
        
    }

    public void Fall()
    {
        _animator.SetTrigger("fall");

    }


}
