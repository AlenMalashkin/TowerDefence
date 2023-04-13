using System;
using UnityEngine;
using VavilichevGD.Utils.Timing;
using Random = UnityEngine.Random;

public class EnemyFactoryInvoker : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private float minTime;
    [SerializeField]  private float maxTime;

    private SyncedTimer _timer;

    private void OnEnable()
    {
        _timer = new SyncedTimer(TimerType.UpdateTick);

        _timer.TimerFinished += InvokeFactory;
        
        _timer.Start(Random.Range(minTime, maxTime));
    }

    private void OnDisable()
    {
        _timer.TimerFinished -= InvokeFactory;
    }

    private void InvokeFactory()
    {
        enemyFactory.CreateEnemy();

        if (minTime > 0.1f)
            minTime -= 0.01f;

        if (maxTime > 0.2f)
            maxTime -= 0.01f;
                
        _timer.Start(Random.Range(minTime, maxTime));
    }
}
