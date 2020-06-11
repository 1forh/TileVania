using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _jumpHeight = 5.5f;

    private float _gravityScaleAtStart;
    
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private CapsuleCollider2D _collider;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _gravityScaleAtStart = _rigidBody.gravityScale;
    }

    void Update()
    {
        Run();
        Jump();
        FlipSprite();
        ClimbLadder();
    }

    private void Run()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var x = horizontalInput * _speed;
        Vector2 position = new Vector2(x, _rigidBody.velocity.y);
        _rigidBody.velocity = position;

        bool playerHasHorizontalSpeed = Mathf.Abs(_rigidBody.velocity.x) > Mathf.Epsilon;
        _animator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump()
    {

        if (!_collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        var spaceInput = Input.GetKeyDown(KeyCode.Space);


        if (spaceInput)
        {
            Vector2 jumpVelocity = new Vector2(0f, _jumpHeight);
            _rigidBody.velocity += jumpVelocity;
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_rigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidBody.velocity.x), 1.0f);
        }
    }

    private void ClimbLadder()
    {
        if (!_collider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            _animator.SetBool("Climbing", false);
            _rigidBody.gravityScale = _gravityScaleAtStart;
            return;
        }

        var verticalInput = Input.GetAxis("Vertical");
        var y = verticalInput * _speed;
        Vector2 position = new Vector2(_rigidBody.velocity.x, y);
        _rigidBody.velocity = position;
        _rigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(_rigidBody.velocity.y) > Mathf.Epsilon;
        _animator.SetBool("Climbing", playerHasVerticalSpeed);
    }
}
