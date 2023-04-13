using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IEnemyFactory
{
    public event Action<Enemy> EnemyCreatedEvent;

    [SerializeField] private DisplayEnemyKilledCount killedCount;
    [SerializeField] private EnemyContainer enemyContainer;
    [SerializeField] private Enemy enemyPrefab;
    public TowerType type;
    
    [Header("Stats")]
    [SerializeField] private int damage;
    [SerializeField] private int maxHealth;
    [SerializeField] private float attackRate;
    
    public EnemyContainer EnemyContainer => enemyContainer;
    
    private Tower[] _towers;

    private Dictionary<TowerType, Tower> _towerTypeMap;

    private void OnEnable()
    { 
        killedCount.OnEnemyKilledCountChangedEvent += OnEnemyKilledCountChanged;
    }

    private void OnDisable()
    {
        killedCount.OnEnemyKilledCountChangedEvent -= OnEnemyKilledCountChanged;
    }

    public void Init(Tower[] towers)
    {
        _towers = towers;
        
        _towerTypeMap = new Dictionary<TowerType, Tower>();

        foreach (var tower in _towers)
        {
            if (!_towerTypeMap.ContainsKey(tower.type))
                _towerTypeMap.Add(tower.type, tower);
        }
    }

    public Enemy CreateEnemy()
    {
        var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemyContainer.transform);
        enemy.SetTargetTower(_towerTypeMap[type]);
        enemy.SetEnemyStats(damage, maxHealth, attackRate);
        EnemyCreatedEvent?.Invoke(enemy);
        return enemy;
    }

    private void OnEnemyKilledCountChanged(int count)
    {
        if (count % 100 == 0 && count != 0)
        {
            if (damage < 10)
                damage += 1;
            
            if (maxHealth < 100)
                maxHealth += 5;
            
            if (attackRate > 0.2f)
                attackRate -= 0.1f;
        }
    }
}
