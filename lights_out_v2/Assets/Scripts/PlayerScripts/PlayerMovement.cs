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
    public Transform attackPoint;
    public LayerMask enemyLayer;


    [Space]
    [Header("Player Private References")]
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    
    [Space]
    [Header("Player Public Statistics")]
    public float movementSpeed;
    public float startTimeBtwAttacks;
    public float attackDamage;
    public float attackRange;

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
        _timeBtwAttacks = 0;
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
            _movement = Vector2.zero;
            _rigidbody.velocity = _movement;
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




        if (_timeBtwAttacks <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StopAllCoroutines();
                currentState = PlayerState.attacking;
                _movement = Vector2.zero;
                _animator.SetTrigger("Attack");
                StartCoroutine(Attack());
                _timeBtwAttacks = startTimeBtwAttacks;
            }
        }
        else
        {
            _timeBtwAttacks -= Time.deltaTime;
        }



        _movement.Normalize();
        
        

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
        yield return new WaitForSeconds(.3f);
        Collider2D[] hitArray = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D collider in hitArray)
        {
            if (collider.CompareTag("Enemy"))
            {
                if (collider.gameObject.GetComponent<Enemy>())
                {
                    Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(attackDamage, GetComponent<Collider2D>());
                }
                else if (collider.gameObject.GetComponentInParent<Enemy>())
                {
                    Enemy enemy = collider.gameObject.GetComponentInParent<Enemy>();
                    enemy.TakeDamage(attackDamage, GetComponent<Collider2D>());
                }
                

            }
        }
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
    private void OnDrawGizmosSelected()
    {
        if (attackPoint.position == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
