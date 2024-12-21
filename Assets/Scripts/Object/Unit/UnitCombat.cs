using System.Linq;
using Assets.Scripts.Data;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private UnitController unitController;
    private string targetTag = Tags.PlayerUnit;
    private UnitManager unitManager;

    public UnitController target { get; private set; }

    private void Start()
    {
        unitManager = UnitManager.Instance;
        targetTag = CompareTag(Tags.PlayerUnit) ? Tags.EnemyUnit : Tags.PlayerUnit;
    }

    private void Update()
    {
        if (!unitController.IsAlive())
        {
            target = null;
            return;
        }

        CheckCurrentTarget();
        foreach (var unit in from unit in unitManager.UnitsOnMap
                 where unit.CompareTag(targetTag)
                 select unit)
            GetTargetInRange(unit);
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

        // Debug.Log(target.gameObject.activeInHierarchy);
        if (Vector3.Distance(transform.position, target.transform.position) > attackRange
            || target.IsDead || !target.gameObject.activeInHierarchy)
        {
            Debug.Log("Running here");
            target = null;
        }
    }

    private void GetTargetInRange(UnitController unit)
    {
        if (Vector3.Distance(transform.position, unit.transform.position) <= attackRange
            && target == null && !unit.IsDead && unit.gameObject.activeInHierarchy)
            target = unit;
    }
}