using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatformPatrolAI : PlatformPatrolAI, IDamageable
{
    [SerializeField] private int _maxHealth = 5;
    private int _currentHealth;

    private float _dazedTime;
    [SerializeField] float _startDazedTime;

    private SpriteRenderer _sprite;
    private bool isGrounded = false;
    public static Vector3 _start_position;

    [SerializeField] private bool isCollidePlayer = false;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayer;

    private int attackDamage = 1;
    [SerializeField] private float attackRange = 0.5f;

    override protected void Start()
    {
        base.Start();
        _currentHealth = _maxHealth;
        _start_position = transform.position;
        GetComponent<BoxCollider2D>().enabled = true;

        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        animator.SetBool(Names.IsLive, true);

        _sprite = GetComponent<SpriteRenderer>();
        Color color = _sprite.material.color;
        color.a = 1f;
        _sprite.material.color = color;
    }
    override protected void Update()
    {
        base.Update();
        if (_currentHealth > 0)
        {
            if (_dazedTime <= 0)
                speed = 2f;
            else
            {
                speed = 0;      
                _dazedTime -= Time.deltaTime;
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Hero hero))
        {
            isCollidePlayer = true;
            animator.SetTrigger(Names.Attack);
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Hero hero))
            isCollidePlayer = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Hero hero))
            isCollidePlayer = false;
    }

    public void ApplyDamage(int _damage)
    {
        animator.SetTrigger(Names.Damage);
        _currentHealth -= _damage;
        _dazedTime = _startDazedTime;

        if (_currentHealth <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        StartCoroutine(CoroutineDie());
    }

    public void DieFromThorns()
    {

    }

    IEnumerator CoroutineDie()
    {
        speed = 0;
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool(Names.IsLive, false);
        yield return new WaitForSeconds(3f);
        StartCoroutine(InvisibleSprite());

    }
    IEnumerator InvisibleSprite()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color color = _sprite.material.color;
            color.a = f;
            _sprite.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }

    protected virtual void PlayerApplyDamage()
    {
        if (isCollidePlayer)
        {
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
            foreach (Collider2D damageableObject in hitObjects)
            {
                //StartCoroutine(Wait());
                damageableObject.GetComponent<Hero>().ApplyDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}