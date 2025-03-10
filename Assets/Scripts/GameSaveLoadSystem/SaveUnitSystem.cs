using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class SaveUnitSystem : MonoBehaviour
{
    public static SaveUnitSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static List<UnitData> SaveUnits()
    {
        var unitsActivedOnMap = UnitPooling.Instance.GetObjectsToSave();
        var unitDatas = new List<UnitData>();
        foreach (var unitType in unitsActivedOnMap)
        foreach (var unit in unitType.Value)
        {
            var data = new UnitData
            {
                Obj = new ObjectData(),
                Stat = new StatData(),
                Position = new PositionData(),
                Rotation = new RotationData(),
                ParticleData = new ExplodeParticleData()
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