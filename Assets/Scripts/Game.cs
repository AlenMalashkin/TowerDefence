using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    
    [Header("Scene Objects")]
    [SerializeField] private Tower[] towers;
    [SerializeField] private EnemyFactory[] factories;
    [SerializeField] private DisplayEnemyKilledCount displayEnemyKilledCount;

    private void OnEnable()
    {
        SoundPlayer.Instance.PlayMusic(music);
        
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
        
        SoundPlayer.Instance.StopMusic();
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
        Debug.Log("Game ended");
        
        if (PlayerPrefs.GetInt("Record") < displayEnemyKilledCount.EnemyKilledCount || !PlayerPrefs.HasKey("Record"))
        {
            PlayerPrefs.SetInt("Record", displayEnemyKilledCount.EnemyKilledCount);
        }

        PlayerPrefs.DeleteKey("Money");
        SceneManager.LoadScene("End");
    }
}
