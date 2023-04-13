using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private DisplayEnemyKilledCount displayEnemyKilledCount;
    
    private List<Enemy> _enemiesInContainer = new List<Enemy>();

    private void OnEnable()
    {
        enemyFactory.EnemyCreatedEvent += EnemyCreated;
    }

    private void OnDisable()
    {
        enemyFactory.EnemyCreatedEvent -= EnemyCreated;
    }

    public List<Enemy> GetEnemies() =>
        _enemiesInContainer;
    
    private void EnemyCreated(Enemy enemy)
    {
        enemy.EnemyKilledEvent.AddListener(EnemyKilled);
        _enemiesInContainer.Add(enemy);
    }

    private void EnemyKilled(Enemy enemy)
    {
        _enemiesInContainer.Remove(enemy);
        displayEnemyKilledCount.UpdateEnemyKilledCount();
    }
}
