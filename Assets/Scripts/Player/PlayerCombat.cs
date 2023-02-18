using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerAnimator animator;
    [SerializeField] private PlayerMovement mov;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    private int attackDamage = 1;
    [SerializeField] private float attackRange = 0.5f;
    private int maxAttacks;

    public int Attacks { get; private set; }

    public bool IsAttacking { get; private set; }

    public bool IsStrongAttacking { get; private set; }

    private bool attackLock;

    


    private void Start()
    {
        IsAttacking = false;
        Attacks = 1;
        maxAttacks = 3;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && mov.RB.velocity.y < -5)
        {
            attackLock = !animator.IsSquating;
            if (attackLock)
            {
                IsAttacking = true;
                mov.SetStaticRB();
                StartCoroutine(AttackInTheFall());
                return;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            attackLock = !animator.IsSquating;
            if (attackLock)
            {
                IsAttacking = true;
                mov.SetStaticRB();
                AttackEnemy(attackDamage);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            attackLock = !animator.IsSquating;
            if (attackLock && mov.LastOnGroundTime == 0.2f)
            {
                IsStrongAttacking = true;
                mov.SetStaticRB();
                StartCoroutine(StrongAttack());
            }
        }
    }

    private IEnumerator StrongAttack()
    { 
        yield return new WaitForSeconds(0.54f);
        AttackEnemy(attackDamage * 2);
    }

    private IEnumerator AttackInTheFall()
    {
        yield return new WaitForSeconds(0.45f);
        AttackEnemy(attackDamage);
    }

    private void AttackEnemy(int attackDamage)
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D damageableObject in hitObjects)
        {
            //StartCoroutine(Wait());
            damageableObject.GetComponent<IDamageable>().ApplyDamage(attackDamage);
        }
    }

    //IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(1f);
    //}
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void EndAttack(int attack)
    {
        IsAttacking = false;
        Attacks++;
        if (attack >= maxAttacks) Attacks = 1;
    }

    public void EndStrongAttack()
    {
        IsAttacking = false;
        IsStrongAttacking = false;
    }

}
