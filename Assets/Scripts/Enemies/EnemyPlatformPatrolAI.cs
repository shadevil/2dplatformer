using System.Collections;
using UnityEngine;

public class EnemyPlatformPatrolAI : PlatformPatrolAI, IDamageable
{
    [SerializeField] private int maxHealth = 5;
    private int currentHealth;

    private bool isLive = true;

    private float dazedTime;
    [SerializeField] float startDazedTime;

    private SpriteRenderer sprite;
    private bool isGrounded = false;
    public static Vector3 startPosition;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayer;

    private int attackDamage = 1;
    [SerializeField] private float attackRange = 0.5f;

    override protected void Start()
    {
        base.Start();
        currentHealth = maxHealth;
        startPosition = transform.position;
        GetComponent<BoxCollider2D>().enabled = true;

        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        animator.SetBool(Names.IsLive, true);

        sprite = GetComponent<SpriteRenderer>();
        Color color = sprite.material.color;
        color.a = 1f;
        sprite.material.color = color;
    }
    override protected void Update()
    {
        base.Update();
        if (currentHealth > 0)
        {
            if (dazedTime <= 0)
                speed = startSpeed;
            else
            {
                speed = 0;      
                dazedTime -= Time.deltaTime;
            }
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Hero hero))
        {
            animator.SetTrigger(Names.Attack);
        }
    }

    //protected void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.TryGetComponent(out Hero hero))
    //    {
    //        isCollidePlayer = true;
    //        animator.SetTrigger(Names.Attack);
    //    }
    //}

    public void ApplyDamage(int _damage)
    {
        isLive = currentHealth > 0;

        if (isLive)
        {       
            animator.SetTrigger(Names.Damage);
            currentHealth -= _damage;
            dazedTime = startDazedTime;
        }

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    protected virtual void PlayerApplyDamage()
    {
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
            foreach (Collider2D damageableObject in hitObjects)
            {
                //StartCoroutine(Wait());
                damageableObject.GetComponent<Hero>().ApplyDamage(attackDamage);
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
            Color color = sprite.material.color;
            color.a = f;
            sprite.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}