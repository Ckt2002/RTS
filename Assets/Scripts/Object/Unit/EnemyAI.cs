using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private UnitCombat combat;
    [SerializeField] private UnitMovement movement;
    [SerializeField] private UnitController controller;

    [SerializeField] private float sensorRange;
    [SerializeField] private List<GameObject> playerHqs;

    [SerializeField] private GameObject mainTarget;

    // main target is nearest player HQ
    // if don't have any player unit or building in sensor range,
    // set current target and move to main target, then destroy it
    // else, change current target to player unit or building

    private void Start()
    {
        FindAllPlayerHQ();
    }

    private void Update()
    {
        if (!controller.IsAlive())
            return;

        FindNearestMainTarget();
        if (mainTarget) movement.Move(mainTarget.transform.position, combat.AttackRange);
    }

    private void FixedUpdate()
    {
        FindAllPlayerHQ();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sensorRange);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Tags.PlayerUnit) || col.CompareTag(Tags.PlayerBuilding))
        {
            if (col.GetComponentInParent<ObjectInfor>().CurrentHealth <= 0)
                return;

            if (combat.target == null || combat.target.GetComponent<ObjectInfor>().CurrentHealth <= 0)
            {
                combat.target = col.gameObject.GetComponent<ObjectInfor>();
                mainTarget = col.gameObject;
            }
        }
    }

    private void FindAllPlayerHQ()
    {
        playerHqs = FindObjectsOfType<GameObject>()
            .Where(obj => obj.name.Contains("Player Headquarters"))
            .ToList();
    }

    // Find nearest player HQ
    private void FindNearestMainTarget()
    {
        if (playerHqs.Count == 0)
            return;

        var targetTemp = playerHqs[0];
        foreach (var HQ in playerHqs)
        {
            var nearest = Vector3.Distance(transform.position, targetTemp.transform.position);
            var nearestTemp = Vector3.Distance(transform.position, HQ.transform.position);
            if (nearest > nearestTemp) targetTemp = HQ;
        }

        if (mainTarget == null)
        {
            mainTarget = targetTemp;
        }
        else
        {
            var mainTargetComponent = mainTarget.GetComponent<ObjectInfor>();
            if (mainTargetComponent == null || mainTargetComponent.CurrentHealth <= 0f) mainTarget = targetTemp;
        }
    }
}