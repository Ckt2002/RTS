using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float stoppingDistance = 0.001f;
    [SerializeField] private UnitInfor stat;

    private Vector3 targetPos;

    private void Awake()
    {
        agent.speed = stat.Speed;
        targetPos = agent.transform.position;
    }

    public void Move()
    {
        agent.SetDestination(targetPos);

        if (!agent.pathPending)
            if (agent.remainingDistance <= stoppingDistance)
                agent.isStopped = true;
    }

    public void SetTargetPosition(Vector3 targetPosition, float stoppingDistance)
    {
        if (agent.isStopped)
            agent.isStopped = false;

        targetPos = targetPosition;
        this.stoppingDistance = stoppingDistance;
    }
}