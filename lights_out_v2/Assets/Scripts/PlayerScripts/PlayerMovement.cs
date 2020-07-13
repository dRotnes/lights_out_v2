using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    walking,
    attacking,
    blocked,
    receiving,
    dead

}
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Public References")]
    public PlayerState currentState;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;


    [Space]
    [Header("Player Private References")]
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    
    [Space]
    [Header("Player Public Statistics")]
    public float movementSpeed;
    public float startTimeBtwAttacks;

    [Space]
    [Header("Player Private Statistics")]
    private Vector2 _movement;
    private float _base_speed;
    private float _timeBtwAttacks;
    private bool _canMove;
    private bool _isInteracting;

    private void Awake() {
        currentState = PlayerState.walking;
        _canMove = true;
        _timeBtwAttacks = startTimeBtwAttacks;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void Update() {
        if (currentState != PlayerState.dead)
        {
            if (_canMove)
                GetInputs();
            else
                return;
        }
        else
        {
            this.enabled = false;
        }
    }

    void GetInputs()
    {

        //Walking Part
        if(currentState == PlayerState.walking)
        {

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            _movement = new Vector2(horizontalInput, verticalInput);
            _base_speed = Mathf.Clamp(_movement.magnitude, 0.0f, 1.0f);
        
        }
        else
        {
            _movement = Vector2.zero;
        }

        if (currentState != PlayerState.attacking && Input.GetButtonDown("Fire1"))
        {
           
            StartCoroutine(Attack());
            
        }
        
        _movement.Normalize();

        
            
           /* if (_timeBtwAttacks < 0)
            {
                StartCoroutine(Attack());
                _timeBtwAttacks = startTimeBtwAttacks;
            }
            
        }
        else
        {
            _timeBtwAttacks -= Time.deltaTime;

        }*/
        
        

    }
    private void FixedUpdate()
    {
        
        _rigidbody.velocity = _movement * movementSpeed * _base_speed;
    }

    private void LateUpdate()
    {
        if(_movement!=Vector2.zero){
            _animator.SetFloat("Horizontal", _movement.x);
            _animator.SetFloat("Vertical", _movement.y);
        }
        _animator.SetFloat("Velocity", _movement.sqrMagnitude);
        _animator.SetBool("Receive", _isInteracting);
    }

    private IEnumerator Attack()
    {
        currentState = PlayerState.attacking;
        _animator.SetBool("Attack", true);
        yield return null;
        _animator.SetBool("Attack", false);
        yield return new WaitForSeconds(.5f);
        currentState = PlayerState.walking;
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {

            if (currentState!=PlayerState.receiving)
            {
                _isInteracting = true;
                currentState = PlayerState.receiving;
                receivedItemSprite.sprite = playerInventory.currentItem.sprite;

            }
            else
            {
                _isInteracting = false;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
                currentState = PlayerState.walking;
            }
        }

    }

    public void BlockOrAllowMovement()
    {
        Debug.Log(_canMove);
        if(_canMove == true)
        {
            _canMove = false;
            _movement = Vector2.zero;
            _rigidbody.velocity = _movement;
        }
        else
        {
            _canMove = true;
        }
    }
    
}
