using System;
using UnityEngine;
using VavilichevGD.Utils.Timing;

public class EnemyAttack : MonoBehaviour
{
    public float AttackCooldown
    {
        get => _attackCooldown;
        set => _attackCooldown = value;
    }

    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }

    private float _attackCooldown;
    private int _damage;

    private bool _isAttacking;
    private float _currentAttackCooldown;
    private SyncedTimer _timer;
    private Tower _target;

    public void EnableAttack(Tower target)
    {
        _target = target;
        _isAttacking = true;
    }

    public void DisableAttack()
    {
        _isAttacking = false;
    }

    private void OnAttack()
    {
        if (_target != null)
            _target.TakeDamage(_damage);
    }

    private void CountCooldown()
    {
         if (_isAttacking) 
             _currentAttackCooldown -= Time.deltaTime;
    }
       

    private bool CooldownIsUp() =>
        _currentAttackCooldown <= 0;

    private bool CanAttack() =>
        CooldownIsUp() && _isAttacking && _target != null;
}
