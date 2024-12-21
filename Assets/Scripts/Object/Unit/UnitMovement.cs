using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float stoppingDistance = 0.001f;
    [SerializeField] private UnitInfor stat;
    [SerializeField] private UnitController unitController;

    private void Start()
    {
        agent.speed = stat.Speed;
    }

    private void Update()
    {
        if (!unitController.IsAlive())
        {
            agent.isStopped = false;
            return;
        }

        if (!agent.pathPending)
            if (agent.remainingDistance <= stoppingDistance)
                agent.isStopped = true;
    }

    public void Move(Vector3 targetPosition)
    {
        if (agent.isStopped)
            agent.isStopped = false;
        agent.SetDestination(targetPosition);
    }
}