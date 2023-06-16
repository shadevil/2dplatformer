using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPlayerDamageable
{
    void ApplyDamage(int damageValue, bool isFasingRight);
    void Die();
    void DieFromThorns();

}