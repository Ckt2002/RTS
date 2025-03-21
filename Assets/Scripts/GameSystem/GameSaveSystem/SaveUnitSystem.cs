using System.Collections.Generic;
using GameSave;

public static class SaveUnitSystem
{
    public static List<UnitData> SaveUnits()
    {
        var unitsActivedOnMap = UnitPooling.Instance.GetObjectsToSave();
        var unitDatas = new List<UnitData>();
        foreach (var unitType in unitsActivedOnMap)
        foreach (var unit in unitType.Value)
        {
            var moveComponent = unit.Value.GetComponent<UnitMovement>();
            var data = new UnitData
            {
                Obj = new ObjectData(),
                Stat = new StatData(),
                Position = new PositionData(),
                Rotation = new RotationData(),
                TargetPosition = new PositionData(),
                Velocity = new PositionData()
            };
            data.Obj.Name = unitType.Key;
            data.Obj.LstIndex = unit.Key;
            data.Stat.CurrentHealth = unit.Value.GetComponent<ObjectInfor>().CurrentHealth;
            data.Position.GetPosition(unit.Value.transform.position);
            data.Rotation.GetRotation(unit.Value.transform.localRotation);
            data.TargetPosition.GetPosition(moveComponent.targetPos);
            data.Velocity.GetPosition(moveComponent.velocity);
            data.IsMoving = moveComponent.isMoving;
            if (data.Stat.CurrentHealth <= 0) data.DieData = unit.Value.GetComponent<ObjectDieStatus>().SaveProgress();
            unitDatas.Add(data);
        }

        return unitDatas;
    }
}