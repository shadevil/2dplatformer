using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IPlayerDamageable
{
    [SerializeField] private float health = 5;
    private Rigidbody2D _rb;
    [SerializeField] private Vector2 pushingForse;
    private PlayerAnimator animator;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        animator = GetComponent<PlayerAnimator>();
    }

    public void ApplyDamage(int damageValue, bool isFasingRight)
    {
        if(isFasingRight)
        _rb.AddForce(new Vector2(pushingForse.x, pushingForse.y), ForceMode2D.Impulse);
        else
            _rb.AddForce(new Vector2(-pushingForse.x, pushingForse.y), ForceMode2D.Impulse);
        health -= damageValue;
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




