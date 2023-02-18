using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerAnimator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    private int attackDamage = 1;
    [SerializeField] private float attackRange = 0.5f;
    private int maxAttacks;

    public int Attacks { get; private set; }

    public bool IsAttacking { get; private set; }

    private bool attackLock;
    private void Start()
    {
        IsAttacking = false;
        Attacks = 1;
        maxAttacks = 3;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attackLock = !animator.IsSquating;
            if (attackLock)
            {
                IsAttacking = true;
                AttackEnemy();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("mouse1");
        }
       
    }

    private void AttackEnemy()
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

}
