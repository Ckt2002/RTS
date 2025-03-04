using System.Collections;
using Assets.Scripts.Data;
using UnityEngine;

public class EnemyUnitController : UnitController
{
    private static GameObject mainTarget;

    /*
     * Main target: player HQ. D
     * If enemy find any player building or unit on path, stop and destroy it.
     * If main target destroyed: Find nearest remain target.
     * If no target remain, do nothing.
     * Find target then send it to combat controller
     */
    [SerializeField] private GameObject currentTarget;

    private void Start()
    {
        StartCoroutine(FindPlayerHQ());
    }

    private static IEnumerator FindPlayerHQ()
    {
        while (true)
        {
            if (mainTarget != null && mainTarget.TryGetComponent(out ObjectInfor comp))
                if (!comp.IsAlive())
                    mainTarget = null;

            var playerObjects = GameObject.FindGameObjectsWithTag(Tags.PlayerBuilding.ToString());
            foreach (var playerObject in playerObjects)
                if (playerObject.name.Contains(Names.PlayerHeadquarter))
                {
                    mainTarget = playerObject;
                    break;
                }

            yield return new WaitForSeconds(0.02f);
        }
    }

    protected override void MovementCalculator()
    {
        var targetTransform = currentTarget ? currentTarget.transform.position : transform.position;
        var stoppingDistance = currentTarget ? stat.AttackRange : 0.0001f;
        movement.SetTargetPosition(targetTransform, stoppingDistance);
        movement.Move();
    }

    protected override void CombatCalculator()
    {
        var comp = combat.FindNearestTargetInMap();
        if (comp != null)
            currentTarget = comp.gameObject;
        else
            currentTarget = mainTarget;

        if (currentTarget != null)
            if (combat.CheckTargetInRange(currentTarget))
                aiming.RotateGun(currentTarget.GetComponent<ObjectInfor>());
        // else
        //     Debug.Log("Out of Range");

        gun.CoolDownCalculator();
    }
}