using System.Collections;
using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class LoadUnitSystem
{
    public static IEnumerator LoadUnit(List<UnitData> unitDatas)
    {
        Debug.Log("Running here");
        var unitDictionary = UnitPooling.Instance.GetObjectDictionary;
        Debug.Log(unitDictionary.Count);
        foreach (var data in unitDatas)
        {
            Debug.Log($"Loaded {data.Obj.Name}");
            var unit = unitDictionary[data.Obj.Name][data.Obj.LstIndex];
            unit.transform.position = data.Position.SetPosition();
            unit.transform.localRotation = data.Rotation.SetRotation();
            unit.GetComponent<ObjectInfor>().CurrentHealth = data.Stat.CurrentHealth;
            unit.GetComponent<UnitController>().SetParticle(data.ParticleData.RunTime);
            unit.SetActive(true);
        }

        yield return null;
    }
}