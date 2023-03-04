using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hero : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 5;
    //private Rigidbody2D _rb;
    [SerializeField] private Vector2 pushingForse;
    [SerializeField] private PlayerAnimator animator;

    private void Awake()
    {
        //_rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyDamage(int damage)
    {
        //_rb.AddForce(new Vector2(pushingForse.x * Input.GetAxis(Names.Horizontal), pushingForse.y), ForceMode2D.Impulse);
        health -= damage;
        animator.ChangeAnimationState(Names.Damage);

        if (health <= 0)
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




