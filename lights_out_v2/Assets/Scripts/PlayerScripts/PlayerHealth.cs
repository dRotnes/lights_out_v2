using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SignalSend playerHealthSignal;
    public float startTimeInvencible;
    public Player playerStats;

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
            
            playerStats.currentHealth -= damage;
            if (playerStats.currentHealth <= 0)
            {
                playerHealthSignal.RaiseSignal();
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
            CinemachineShake.Instance.ShakeCam(1f, .2f);
            playerHealthSignal.RaiseSignal();
        }
    }
    public void AddHealth(float healthAdded)
    {
        if(playerStats.currentHealth + healthAdded >= playerStats.maxHealth)
        {
            playerStats.currentHealth = playerStats.maxHealth;
        }
        else
            playerStats.currentHealth += healthAdded;
        playerHealthSignal.RaiseSignal();
    }
    public void SetMaxHealth()
    {
        playerStats.maxHealth += 2;
        playerStats.currentHealth = playerStats.maxHealth;
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
        
    }

    public void Fall()
    {
        _animator.SetTrigger("fall");
    }


}
