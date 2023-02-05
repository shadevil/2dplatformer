using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayers;

    private int _attackDamage = 1;
    [SerializeField] private float _attackRange = 0.5f;
    private int maxAttacks;

    public int Attacks { get; private set; }

    public bool IsAttacking { get; private set; }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        IsAttacking = false;
        Attacks = 1;
        maxAttacks = 3;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsAttacking = true;
            AttackEnemy();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("mouse1");
        }
       
    }

    private void AttackEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            //StartCoroutine(Wait());
            enemy.GetComponent<Enemy>().TakeDamageEnemy(_attackDamage);
        }
    }

    //IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(1f);
    //}
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public void EndAttack(int attack)
    {
        IsAttacking = false;
        Attacks++;
        if (attack >= maxAttacks) Attacks = 1;
    }
}
