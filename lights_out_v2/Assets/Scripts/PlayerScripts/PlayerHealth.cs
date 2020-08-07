using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SignalSend playerHealthSignal;
    public FloatValue currentHealth;
    public Collider2D footCollider;
    public float startTimeInvencible;

    private Color _blinkColor;
    private float _timeInvencible;
    private Material _mat;
    private PlayerMovement _playerMovement;
    private bool _isInvencible;
    private Animator _animator;
    private SpriteRenderer _sr;
    private float _maxHealth;

    private void Awake()
    {
        _timeInvencible = startTimeInvencible;
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _mat = GetComponent<SpriteRenderer>().material;
        _maxHealth = currentHealth.initialValue;
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
            currentHealth.RuntimeValue -= damage;
            if (currentHealth.RuntimeValue <= 0)
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
        if(currentHealth.RuntimeValue + healthAdded >= _maxHealth)
        {
            currentHealth.RuntimeValue = _maxHealth;
        }
        else
            currentHealth.RuntimeValue += healthAdded;
        playerHealthSignal.RaiseSignal();
    }
    public void SetMaxHealth()
    {
        _maxHealth += 2;
        AddHealth(_maxHealth);
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
