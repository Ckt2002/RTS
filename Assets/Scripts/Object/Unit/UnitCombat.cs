using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    [SerializeField] private UnitInfor stat;

    private BuildingPooling buildingPooling;
    private ObjectInfor target;
    private Tags targetBuildingTag = Tags.PlayerBuilding;
    private Tags targetUnitTag = Tags.PlayerUnit;
    private UnitPooling unitPooling;

    private void Start()
    {
        unitPooling = UnitPooling.Instance;
        buildingPooling = BuildingPooling.Instance;
        targetUnitTag = CompareTag(Tags.PlayerUnit.ToString()) ? Tags.EnemyUnit : Tags.PlayerUnit;
        targetBuildingTag = CompareTag(Tags.PlayerUnit.ToString()) ? Tags.EnemyBuilding : Tags.PlayerBuilding;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stat.AttackRange);
    }

    public ObjectInfor FindTargetInRange()
    {
        CheckCurrentTarget();

        foreach (var unit in CombineUnits())
            if (unit.CompareTag(targetUnitTag.ToString()) || unit.CompareTag(targetBuildingTag.ToString()))
                GetTargetInRange(unit.GetComponent<ObjectInfor>());

        return target;
    }

    public ObjectInfor FindNearestTargetInMap()
    {
        CheckCurrentTarget();

        var nearestTarget = Mathf.Infinity;
        foreach (var unit in CombineUnits())
            if (unit.CompareTag(targetUnitTag.ToString()) || unit.CompareTag(targetBuildingTag.ToString()))
            {
                var distance = Vector3.Distance(transform.position, unit.transform.position);
                if (distance < nearestTarget)
                {
                    nearestTarget = distance;
                    target = unit.GetComponent<ObjectInfor>();
                }
            }

        return target;
    }

    private List<GameObject> CombineUnits()
    {
        return unitPooling.GetAllActiveObject()
            .Concat(buildingPooling.GetAllActiveObject())
            .ToList();
    }

    private void CheckCurrentTarget()
    {
        if (target == null)
            return;

        if (Vector3.Distance(transform.position, target.transform.position) > stat.AttackRange
            || target.CurrentHealth <= 0 || !target.gameObject.activeInHierarchy)
            target = null;
    }

    private void GetTargetInRange(ObjectInfor obj)
    {
        if (Vector3.Distance(transform.position, obj.transform.position) <= stat.AttackRange
            && target == null && obj.CurrentHealth > 0 && obj.gameObject.activeInHierarchy)
            target = obj;
    }

    public bool CheckTargetInRange(GameObject currentTarget)
    {
        return Vector3.Distance(transform.position, currentTarget.transform.position) <= stat.AttackRange;
    }
}