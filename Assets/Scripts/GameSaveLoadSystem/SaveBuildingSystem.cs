using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class SaveBuildingSystem : MonoBehaviour
{
    public static List<BuildingData> SaveUnits()
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
                Rotation = new RotationData(),
                ParticleData = new ExplodeParticleData(),
                BuyOrResearch = new BuyOrResearchData()
            };
            data.Obj.Name = unitType.Key;
            data.Obj.LstIndex = unit.Key;
            data.Stat.CurrentHealth = unit.Value.GetComponent<ObjectInfor>().CurrentHealth;
            data.Position.GetPosition(unit.Value.transform.position);
            data.Rotation.GetRotation(unit.Value.transform.localRotation);
            data.ParticleData.RunTime = unit.Value.GetComponentInChildren<ParticleSystem>().time;

            unitDatas.Add(data);
        }

        return unitDatas;
    }
}