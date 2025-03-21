using System.Collections.Generic;
using GameSave;

public class SaveBuildingSystem
{
    public static List<BuildingData> SaveBuildings()
    {
        var unitsActivedOnMap = BuildingPooling.Instance.GetObjectsToSave();
        var unitDatas = new List<BuildingData>();
        foreach (var unitType in unitsActivedOnMap)
        foreach (var unit in unitType.Value)
        {
            var data = new BuildingData
            {
                Obj = new ObjectData(),
                Stat = new StatData(),
                Position = new PositionData(),
                Rotation = new RotationData()
            };
            data.Obj.Name = unitType.Key;
            data.Obj.LstIndex = unit.Key;
            data.Stat.CurrentHealth = unit.Value.GetComponent<ObjectInfor>().CurrentHealth;
            data.Position.GetPosition(unit.Value.transform.position);
            data.Rotation.GetRotation(unit.Value.transform.localRotation);
            if (data.Stat.CurrentHealth <= 0) data.DieData = unit.Value.GetComponent<ObjectDieStatus>().SaveProgress();
            unitDatas.Add(data);
        }

        return unitDatas;
    }
}