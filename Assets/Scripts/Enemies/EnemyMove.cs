using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float stoppingDistance;
    
    private NavMeshAgent _agent;

    public void Move(Vector3 targetPosition)
    {
        _agent.enabled = true;
        _agent.SetDestination(targetPosition);
        
        if (_agent.remainingDistance < stoppingDistance && _agent.remainingDistance > 0)
        {
            Stop();
        }
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Stop()
    {
        _agent.enabled = false;
    }
}
