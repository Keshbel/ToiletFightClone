using UnityEngine;
using UnityEngine.AI;

public class Moveable : MonoBehaviour
{
    public NavMeshAgent Agent { get; set; }
    public bool isChasing = false;
    [SerializeField] private Transform currentTarget;

    private void Update()
    {
        if (isChasing)
        {
            Agent.SetDestination(currentTarget.position);
            Agent.transform.LookAt(currentTarget);
        }
    }

    public void ChaseTarget(Transform target)
    {
        currentTarget = target;
        isChasing = true;
    }

    public void StopChasing()
    {
        Agent.isStopped = true;
        isChasing = false;
        Agent.transform.LookAt(currentTarget);
    }

    public void RunningToFinish()
    {
        isChasing = false;
        Agent.isStopped = false;
    }
}
