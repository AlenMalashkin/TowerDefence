using UnityEngine;
using VavilichevGD.Utils.Timing;
using Random = UnityEngine.Random;

public class EnemyFactoryInvoker : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private float timeToDecreaceInvoke;
    [SerializeField] private float minTime;
    [SerializeField]  private float maxTime;

    private SyncedTimer _factoryInvokeTimer;
    private SyncedTimer _decreaceInvokeTimer;

    private void OnEnable()
    {
        _factoryInvokeTimer = new SyncedTimer(TimerType.UpdateTick);
        _decreaceInvokeTimer = new SyncedTimer(TimerType.UpdateTick);

        _factoryInvokeTimer.TimerFinished += InvokeFactory;
        _decreaceInvokeTimer.TimerFinished += DecreaceInvokeTime;
        
        _factoryInvokeTimer.Start(Random.Range(minTime, maxTime));
        _decreaceInvokeTimer.Start(timeToDecreaceInvoke);
    }

    private void OnDisable()
    {
        _factoryInvokeTimer.TimerFinished -= InvokeFactory;
        _decreaceInvokeTimer.TimerFinished -= DecreaceInvokeTime;
    }

    private void DecreaceInvokeTime()
    {
        if (minTime > 0.1f)
            minTime -= 0.1f;

        if (maxTime > 0.2f)
            maxTime -= 0.1f;

        _decreaceInvokeTimer.Start(timeToDecreaceInvoke);
    }

    private void InvokeFactory()
    {
        enemyFactory.CreateEnemy();
        
        _factoryInvokeTimer.Start(Random.Range(minTime, maxTime));
    }
}
