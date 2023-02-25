using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hero : MonoBehaviour, IDamageable
{
    [SerializeField] private float lives = 5;
    private Rigidbody2D _rb;
    [SerializeField] private Vector2 pushingForse;
    [SerializeField] private PlayerAnimator animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            
        }
    }

    public void ApplyDamage(int damage)
    {
        //_rb.AddForce(new Vector2(pushingForse.x * Input.GetAxis(Names.Horizontal), pushingForse.y), ForceMode2D.Impulse);
        lives -= damage;
        animator.ChangeAnimationState(Names.Damage);

        if (lives <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.ChangeAnimationState(Names.Death);
    }

    public void DieFromThorns()
    {
        animator.ChangeAnimationState(Names.DeathFromThorns);
    }
}




