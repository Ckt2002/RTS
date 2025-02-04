using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float stoppingDistance = 0.001f;
    [SerializeField] private UnitInfor stat;
    [SerializeField] private UnitController unitController;

    private Vector3 targetPos;

    private void Start()
    {
        agent.speed = stat.Speed;
        targetPos = agent.transform.position;
    }

    private void Update()
    {
        if (!unitController.IsAlive())
        {
            agent.isStopped = false;
            targetPos = transform.position;
            agent.SetDestination(targetPos);
            return;
        }
        agent.SetDestination(targetPos);

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= stoppingDistance)
                agent.isStopped = true;
        }
    }

    public void Move(Vector3 targetPosition, float stoppingDistance)
    {
        if (agent.isStopped)
            agent.isStopped = false;
        targetPos = targetPosition;
        this.stoppingDistance = stoppingDistance;
    }

    public float StoppingDistance
    {
        get { return agent.stoppingDistance; }
        set { this.stoppingDistance = value; }
    }
}