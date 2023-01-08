using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum States 
{
    Idle,
    Run,
    JumpUp,
    JumpDown,
    Other
}
public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private int _lives = 5;
    [SerializeField] private float _jumpForce = 3f;
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    [SerializeField] private bool isGrounded = false;
    public static Vector3 _start_position;
    private Animator _animator;
    //private Transform _transform;
   
    private States State 
    {
        get { return (States)_animator.GetInteger(Names.State); }
        set { _animator.SetInteger(Names.State, (int)value); }
    }
    private void Start()
    {
        _start_position = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        
    }

    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Update()
    {
        if (isGrounded && State != States.Other) State = States.Idle;

        if (Input.GetButton(Names.Horizontal) && State != States.Other)
        {
            Run();
        }

        if (isGrounded && Input.GetButtonDown(Names.Jump))
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            State = States.Other;
        }

    }

    public void SetState()
    {
        if (Input.GetButton(Names.Horizontal))
        {
            Run();
        }

        if (isGrounded && Input.GetButtonDown(Names.Jump))
        {
            Jump();
        }

        if (isGrounded) State = States.Idle;
    }
    private void Run() 
    {
        if (isGrounded) State = States.Run;

        Vector3 _dir = transform.right * Input.GetAxis(Names.Horizontal);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _dir, _speed * Time.deltaTime);
        _sprite.flipX = _dir.x < 0.0f;

        

    }

    private void Jump() 
    {
        _rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround() 
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.4f);
        isGrounded = collider.Length > 1;

        if (!isGrounded)
        {
            //if(transform.position.y > _prev_val_y)
            State = States.JumpUp;
            //if(transform.position.y < _prev_val_y)
            //State = States.JumpDown;
        }
    }
    //IEnumerator MoreDamage()
    //{
    //    yield return new WaitForSeconds(50f);
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Names.Enemy) 
        {
            Damage();
            Debug.Log("1");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Names.Enemy) 
        {
            //StartCoroutine(MoreDamage());
            Damage();
            Debug.Log("2");
        }
    }

    
    private void Damage()
    {
        _animator.SetTrigger(Names.Damage);
    }

}
