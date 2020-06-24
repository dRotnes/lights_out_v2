using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    idle,
    walking,
    attacking,
    blocked

}
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Public References")]
    public PlayerState currentState;

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

    private void Awake() {
        _canMove = true;
        _timeBtwAttacks = startTimeBtwAttacks;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void Update() {
        GetInputs();

        //Handling States
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            currentState = PlayerState.attacking;
        }
        else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            currentState = PlayerState.walking;
        }
        else
        {
            currentState = PlayerState.idle; ;

        }

        
    }

    void GetInputs()
    {
        if (_canMove)
        {

            //Walking Part
            if (currentState != PlayerState.attacking)
            {
                float horizontalInput = Input.GetAxisRaw("Horizontal");
                float verticalInput = Input.GetAxisRaw("Vertical");
                _movement = new Vector2(horizontalInput, verticalInput);
                _base_speed = Mathf.Clamp(_movement.magnitude, 0.0f, 1.0f);
                _movement.Normalize();
            }

            if (_timeBtwAttacks < 0)
            {
                if (Input.GetButtonDown("Fire1") && currentState != PlayerState.attacking)
                {
                    Attack();
                    _timeBtwAttacks = startTimeBtwAttacks;
                }
            
            }
            else
            {
                _timeBtwAttacks -= Time.deltaTime;

            }
        }
        

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
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public void BlockOrAllowMovement()
    {
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
