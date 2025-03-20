using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float stoppingDistance = 0.001f;
    [SerializeField] private UnitInfor stat;

    public Vector3 targetPos { get; private set; }
    public Vector3 velocity { get; private set; }
    public bool isMoving { get; private set; }

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
            {
                agent.isStopped = true;
                isMoving = false;
            }
    }

    public void SetTargetPosition(Vector3 targetPosition, float stoppingDistance)
    {
        if (agent.isStopped)
            agent.isStopped = false;

        targetPos = targetPosition;
        isMoving = true;
        this.stoppingDistance = stoppingDistance;
    }

    public void Pause()
    {
        agent.isStopped = true;
        velocity = agent.velocity;
    }

    public void Resume()
    {
        if (isMoving)
        {
            agent.isStopped = false;
            agent.velocity = velocity;
            velocity = Vector3.zero;
        }
    }

    public void LoadGame(Vector3 targetPos, Vector3 velocity, bool isMoving)
    {
        this.targetPos = targetPos;
        this.velocity = velocity;
        this.isMoving = isMoving;
    }
}