using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SignalSend playerHealthSignal;
    public Collider2D footCollider;
    public float startTimeInvencible;
    public FloatValue currentHealth;
    public FloatValue maxHealth;

    private Color _blinkColor;
    private float _timeInvencible;
    private Material _mat;
    private PlayerMovement _playerMovement;
    private bool _isInvencible;
    private Animator _animator;
    private SpriteRenderer _sr;

    private void Start()
    {
        _timeInvencible = startTimeInvencible;
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _mat = GetComponent<SpriteRenderer>().material;
    }
    private void Update()
    {
        
        if (_isInvencible)
        {
            
            if (_timeInvencible < 0)
            {
                Debug.Log("Can damage again");
                StopAllCoroutines();
                _blinkColor = new Color(1f, 1f, 1f, 0f);
                _mat.SetColor("_Tint", _blinkColor);
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
            currentHealth.value -= damage;
            if (currentHealth.value <= 0)
            {
                Die();
                return;

            }
            if (enemy)
            {
                _isInvencible = true;
                StartCoroutine(FlashInvencible());
            }
            else
            {
                StartCoroutine(FlashDamage());
            }
            /*FindObjectOfType<AudioManager>().Play("hit_sound");*/
            CinemachineShake.Instance.ShakeCam(0.7f, .1f);
            playerHealthSignal.RaiseSignal();
        }
    }
    public void AddHealth(float healthAdded)
    {
        if(currentHealth.value + healthAdded >= maxHealth.value)
        {
            currentHealth.value = maxHealth.value;
        }
        else
            currentHealth.value += healthAdded;
        playerHealthSignal.RaiseSignal();
    }
    public void SetMaxHealth()
    {
        maxHealth.value += 2;
        currentHealth.value = maxHealth.value;
    }

    private IEnumerator FlashDamage()
    {
        _blinkColor = new Color(1f, 1f, 1f, 1f);
        _mat.SetColor("_Tint", _blinkColor);
        yield return new WaitForSeconds(.1f);
        _blinkColor = new Color(1f, 1f, 1f, 0f);
        _mat.SetColor("_Tint", _blinkColor);
    }
    private IEnumerator FlashInvencible() 
    {
        while (_isInvencible)
        {

            _blinkColor = new Color(1f, 1f, 1f, 1f);
            _mat.SetColor("_Tint", _blinkColor);
            yield return new WaitForSeconds(.1f);
            _blinkColor = new Color(1f, 1f, 1f, 0f);
            _mat.SetColor("_Tint", _blinkColor);
            yield return new WaitForSeconds(.1f);
        }
        _blinkColor = new Color(1f, 1f, 1f, 0f);
        _mat.SetColor("_Tint", _blinkColor);
    }

    private void Die()
    {
        StartCoroutine(FlashDamage());
        _playerMovement.currentState = PlayerState.dead;
        _animator.SetBool("Dead", true);
        footCollider.enabled = false;
        
    }

    public void Fall()
    {
        _animator.SetTrigger("fall");

    }


}
