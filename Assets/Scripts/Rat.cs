using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;
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
        _boxCollider = GetComponent<BoxCollider2D>();
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

        if (_sprite.flipX == true)
            _boxCollider.offset = new Vector2(-0.2216978f, 0.2526783f);
        else 
            _boxCollider.offset = new Vector2(0.7117f, 0.2526783f);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Names.Player)
        {
            _animator.SetTrigger(Names.Attack);
        }
    }
}
