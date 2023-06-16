using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageable 
{
    void ApplyDamage(int damageValue);

    void Die();
}
