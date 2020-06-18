using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _movement;
    public Transform rightCheck, bottomCheck, leftCheck;
    public LayerMask groundLayer;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float collisionRadius;

    [Space]
    [Header("Booleans")]
    private bool _isGround;
    private bool _isWall;

    [Space]

    private bool groundTouch;
    private bool _facingRight = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

       
    }
    void Update()
    {
        //Collision checking
        CheckCollision();

        //Movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _movement = new Vector2(horizontalInput, 0);

         if (horizontalInput < 0f && _facingRight == true)
        {
            Flip();
        }
        else if (horizontalInput > 0f && _facingRight == false)
        {
        
            Flip();
        }

        //Jumping Gravity testing
        if(_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if(_rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //Jumping
        if(Input.GetButtonDown("Jump")){
            if(_isGround){

                Jump();
            }
        }

    }

    private void FixedUpdate()
    {
        float horizontalVelocity = _movement.normalized.x * speed;
        _rb.velocity = new Vector2(horizontalVelocity, _rb.velocity.y);
       
    }

    private void Jump()
    {
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckCollision(){
        _isGround = Physics2D.OverlapCircle(bottomCheck.position, collisionRadius, groundLayer);
        _isWall = Physics2D.OverlapCircle(leftCheck.position, collisionRadius, groundLayer) || Physics2D.OverlapCircle(leftCheck.position, collisionRadius, groundLayer);

        Debug.Log(_isGround);
    }
    private void Flip() {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
