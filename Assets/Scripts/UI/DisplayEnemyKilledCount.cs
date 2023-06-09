using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemyKilledCount : MonoBehaviour
{
    public event Action<int> OnEnemyKilledCountChangedEvent;
    
    [SerializeField] private Text text;

    public int _enemyKilledCount = 0;

    public int EnemyKilledCount => _enemyKilledCount;

    private void Awake()
    {
        text.text = _enemyKilledCount + "";
    }

    public void UpdateEnemyKilledCount()
    {
        _enemyKilledCount += 1;
        text.text = _enemyKilledCount + "";
        OnEnemyKilledCountChangedEvent?.Invoke(_enemyKilledCount);
    }
}
