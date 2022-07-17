using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    [SerializeField] private Vector3 _runTrigger = new Vector3(3,3);
    [SerializeField] private Transform _player;
    private Vector3 _playerPosition;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private int _lives = 5;
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private bool isGrounded = false;
    public static Vector3 _start_position;
    private Animator _animator;

    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Start()
    {
        _start_position = transform.position;
        _playerPosition = _player.position;
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (transform.position.x - _playerPosition.x >= _runTrigger.x)
        {
            Run();
        }
        else
        {
            if(isGrounded) _animator.SetBool(Names.Run, false); 
        }

    }
    private void Run()
    {
        if (isGrounded) _animator.SetBool(Names.Run, true);

        Vector3 _dir = _player.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, _dir, _speed * Time.deltaTime);
        _sprite.flipX = _dir.x - transform.position.x < 0.0f;
    }
    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }

}
