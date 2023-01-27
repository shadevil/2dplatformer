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

    private int numberOfAttacks;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            numberOfAttacks++;
            if (numberOfAttacks > 3)
            {
                numberOfAttacks = 0;
            }
            AttackEnemy();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("mouse1");
        }
       
    }

    private void AttackEnemy()
    {
        _animator.SetTrigger("Attack");
        _animator.SetInteger("NumberOfAttacks", numberOfAttacks);
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
}
