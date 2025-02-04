using System.Collections.Generic;
using UnityEngine;

public class UnitFormation : MonoBehaviour
{
    [SerializeField] private float unitSpacing;
    [SerializeField] private UnitManager unitManager;

    private List<UnitController> unitsSelected;

    public void GetInFormationAndMove(Vector3? targetPosition = null)
    {
        var unitsSelected = unitManager.UnitsSelected;
        if (unitsSelected.Count == 0)
            return;

        var unitCount = unitsSelected.Count;
        var rowColCount = Mathf.CeilToInt(Mathf.Sqrt(unitCount));

        var formationCenter = targetPosition ?? unitsSelected[0].transform.position;
        var moveDirection = targetPosition.HasValue
            ? (targetPosition.Value - unitsSelected[0].transform.position).normalized
            : Vector3.forward;
        var formationRotation = Quaternion.LookRotation(moveDirection);

        for (var index = 0; index < unitsSelected.Count; index++)
        {
            var row = index / rowColCount;
            var col = index % rowColCount;

            var unitNewPos = formationRotation * new Vector3(col * unitSpacing, 0, row * unitSpacing);
            var finalPosition = formationCenter - unitNewPos;

            var unitMovement = unitsSelected[index].GetComponent<UnitMovement>();
            unitMovement.Move(finalPosition, 0f);
            var playerRing = unitsSelected[index].GetComponent<PlayerRing>();
        }
    }

    public void GetInAttackStatusAndMove(Vector3? targetPosition = null)
    {
        var unitsSelected = unitManager.UnitsSelected;
        if (unitsSelected.Count == 0 || targetPosition == null)
            return;

        var unitCount = unitsSelected.Count;

        for (var index = 0; index < unitsSelected.Count; index++)
        {
            var unitMovement = unitsSelected[index].GetComponent<UnitMovement>();
            var unitAttackRange = unitsSelected[index].GetComponent<UnitCombat>().AttackRange;

            unitMovement.Move(targetPosition.Value, unitAttackRange);

            var playerRing = unitsSelected[index].GetComponent<PlayerRing>();
        }
    }
}