using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCombat : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayers;

    private int _attackDamage = 1;
    [SerializeField] private float _attackRange = 0.5f;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackEnemy();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("mouse1");
        }
       
    }

    private void AttackEnemy()
    {   
        _animator.SetTrigger(Names.Attack);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamageEnemy(_attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
