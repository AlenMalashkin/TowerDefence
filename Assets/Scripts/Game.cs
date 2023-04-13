using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("Scene Objects")]
    [SerializeField] private Tower[] towers;
    [SerializeField] private EnemyFactory[] factories;

    private void OnEnable()
    {
        foreach (var tower in towers)
        {
            tower.OnTowerDestroyedEvent += OnTowerDestroyed;
        }

        foreach (var factory in factories)
        {
            factory.Init(towers);
        }
    }

    private void OnDisable()
    {
        foreach (var tower in towers)
        {
            tower.OnTowerDestroyedEvent -= OnTowerDestroyed;
        }
    }

    private void OnTowerDestroyed(TowerType towerType)
    {
        if (towerType == TowerType.Ancient)
        {
            EndGame();
        }
        else
        {
            foreach (var factory in factories)
            {
                if (factory.type == towerType)
                {
                    factory.type = TowerType.Ancient;

                    var enemies = factory
                        .EnemyContainer
                        .GetEnemies();

                    foreach (var enemy in enemies)
                    {
                        enemy.SetTargetTower(Array.Find(towers, tower => tower.type == TowerType.Ancient));
                    }
                }
            }
        }
    }

    private void EndGame()
    {
        PlayerPrefs.DeleteKey("Money");
        Debug.Log("Game ended");   
    }
}
