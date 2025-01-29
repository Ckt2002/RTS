using Assets.Scripts.Data;
using System.Linq;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private UnitController unitController;

    // Change following to use for attacking building too
    private string targetUnitTag = Tags.PlayerUnit;
    private string targetBuildingTag = Tags.PlayerBuilding;
    private UnitManager unitManager;
    private BuildingManager buildingManager;

    public ObjectInfor target { get; set; }

    private void Start()
    {
        unitManager = UnitManager.Instance;
        buildingManager = BuildingManager.Instance;
        targetUnitTag = CompareTag(Tags.PlayerUnit) ? Tags.EnemyUnit : Tags.PlayerUnit;
        targetBuildingTag = CompareTag(Tags.PlayerUnit) ? Tags.EnemyBuilding : Tags.PlayerBuilding;
    }

    private void Update()
    {
        if (!unitController.IsAlive())
        {
            target = null;
            return;
        }

        CheckCurrentTarget();

        var combinedUnits = unitManager.UnitsOnMap
            .Where(unit => unit.CompareTag(targetUnitTag))
            .Concat(buildingManager.BuildingsOnMap.Where(unit => unit.CompareTag(targetBuildingTag)));

        foreach (var unit in combinedUnits)
        {
            GetTargetInRange(unit.GetComponent<ObjectInfor>());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void CheckCurrentTarget()
    {
        if (target == null)
            return;

        if (Vector3.Distance(transform.position, target.transform.position) > attackRange
            || target.CurrentHealth <= 0 || !target.gameObject.activeInHierarchy)
        {
            //Debug.Log("Running here");
            target = null;
        }
    }

    private void GetTargetInRange(ObjectInfor obj)
    {
        if (Vector3.Distance(transform.position, obj.transform.position) <= attackRange
            && target == null && (obj.CurrentHealth > 0) && obj.gameObject.activeInHierarchy)
            target = obj;
    }

    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
}