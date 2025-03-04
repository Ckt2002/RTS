using System.Collections.Generic;
using UnityEngine;

public class UnitFormation : MonoBehaviour
{
    [SerializeField] private float unitSpacing;
    [SerializeField] private UnitManager unitManager;

    private List<UnitController> unitsSelected;

    public void GetInFormationAndMove(Vector3? targetPosition = null)
    {
        var unitsSelectedTemp = unitManager.UnitsSelected;
        if (unitsSelectedTemp.Count == 0)
            return;

        var unitCount = unitsSelectedTemp.Count;
        var rowColCount = Mathf.CeilToInt(Mathf.Sqrt(unitCount));

        var formationCenter = targetPosition ?? unitsSelectedTemp[0].transform.position;
        var moveDirection = targetPosition.HasValue
            ? (targetPosition.Value - unitsSelectedTemp[0].transform.position).normalized
            : Vector3.forward;
        var formationRotation = Quaternion.LookRotation(moveDirection);

        for (var index = 0; index < unitsSelectedTemp.Count; index++)
        {
            var row = index / rowColCount;
            var col = index % rowColCount;

            var unitNewPos = formationRotation * new Vector3(col * unitSpacing, 0, row * unitSpacing);
            var finalPosition = formationCenter - unitNewPos;

            var unitMovement = unitsSelectedTemp[index].GetComponent<UnitMovement>();
            unitMovement.SetTargetPosition(finalPosition, 0f);
        }
    }

    public void GetInAttackStatusAndMove(Vector3? targetPosition = null)
    {
        var unitsSelectedTemp = unitManager.UnitsSelected;
        if (unitsSelectedTemp.Count == 0 || targetPosition == null)
            return;

        foreach (var unit in unitsSelectedTemp)
        {
            var unitMovement = unit.GetComponent<UnitMovement>();
            var unitAttackRange = unit.GetComponent<UnitInfor>().AttackRange;

            unitMovement.SetTargetPosition(targetPosition.Value, unitAttackRange);
        }
    }
}