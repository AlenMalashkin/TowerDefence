using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent<Enemy> EnemyKilledEvent = new UnityEvent<Enemy>();
    public EnemyType type;
    
    [Header("Depends")]
    [SerializeField] private TriggerObserver triggerObserver;
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private EnemyMove enemyMove;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private Lifebar lifebar;

    [Header("Particulars")] 
    [SerializeField] private int amountOfMoney;
    
    private Tower _tower;

    public void SetTargetTower(Tower tower)
    {
        _tower = tower;
    }
    
    public void SetEnemyStats(int damage, int maxHealth, float attackRate)
    {
        enemyAttack.Damage = damage;
        enemyAttack.AttackCooldown = attackRate;
        enemyHealth.MaxHealth = maxHealth;
    }

    private void OnEnable()
    {
        triggerObserver.OnTriggerEnteredEvent += OnTriggerEntered;
        triggerObserver.OnTriggerExitedEvent += OnTriggerExited;
        enemyHealth.OnEnemyHealthValueChanged += OnEnemyHealthChanged;
    }

    private void OnDisable()
    {
        triggerObserver.OnTriggerEnteredEvent -= OnTriggerEntered;
        triggerObserver.OnTriggerExitedEvent -= OnTriggerExited;
        enemyHealth.OnEnemyHealthValueChanged -= OnEnemyHealthChanged;
    }

    private void Update()
    {
        enemyMove.Move(_tower.transform.position);

        enemyAttack.Attack();
    }
    
    private void OnTriggerEntered(Collider other)
    {
        if (other.TryGetComponent(out Tower tower))
        {
            enemyAttack.EnableAttack(tower);
        }
    }

    private void OnTriggerExited(Collider other)
    {
        if (other.TryGetComponent(out Tower tower))
        { 
            enemyAttack.DisableAttack();
            enemyMove.Move(_tower.transform.position); 
        }
    }

    private void OnEnemyHealthChanged(int health)
    {
        if (health <= 0)
            Die();
        
        lifebar.UpdateLifebar(health, enemyHealth.MaxHealth);
    }
    
    private void Die()
    {
        EnemyKilledEvent.Invoke(this);
        Bank.Instance.GetMoney(CurrencyType.Money, amountOfMoney);
        Destroy(gameObject);
    }
}
