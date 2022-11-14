using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private Animator _animator;
    [SerializeField]
    private LayerMask jumpGround;
    private float directionX = 0f;
    [SerializeField]
    private float moveSpeed = 7f;
    [SerializeField]
    private float jumpHigh = 7f;

    private enum PlayerMove {initial, run, jump, fall}
    
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        _rigidBody.velocity = new Vector2(directionX * moveSpeed, _rigidBody.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
            //edit -> projectSettings->input manager
            //jump = space
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpHigh);
        }

        UpdateAnimation();
        
    }
    private void UpdateAnimation()
    {
        PlayerMove _move;
     
        if (directionX < 0f) //la stanga
        {
            _spriteRenderer.flipX = true;

            _move = PlayerMove.run;
        }
        else if (directionX > 0f) //dreapta
        {
            _spriteRenderer.flipX = false;

            _move = PlayerMove.run;
        }
        else
        {
            _move = PlayerMove.initial;
        }

        if (_rigidBody.velocity.y < -.1f)
        {
            _move = PlayerMove.fall;
        }
        else if(_rigidBody.velocity.y > .1f)
        {
            _move = PlayerMove.jump;
        }
        _animator.SetInteger("_move", (int)_move); //transform enum into int
    }

    private bool IsGrounded() // jump just when we are on the terrain not in air
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }
    
}
