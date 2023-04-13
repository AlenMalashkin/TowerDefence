using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public event Action<int> OnEnemyHealthValueChanged;

    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
            _currentHealth = value;
        }
    }

    private int _maxHealth;
    private int _currentHealth;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        OnEnemyHealthValueChanged?.Invoke(_currentHealth);
    }
}
