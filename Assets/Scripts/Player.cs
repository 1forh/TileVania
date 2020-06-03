using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _jumpHeight = 4.5f;

    bool isAlive = true;
    
    private Rigidbody2D _rigidBody;
    private Animator _animator;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        Jump();
        FlipSprite();
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
}
