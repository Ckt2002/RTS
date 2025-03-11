using System.Collections;
using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class LoadUnitSystem : MonoBehaviour
{
    public static IEnumerable LoadUnit(List<UnitData> unitDatas)
    {
        var unitDictionary = UnitPooling.Instance.GetObjectDictionary;
        foreach (var data in unitDatas)
        {
            var unit = unitDictionary[data.Obj.Name][data.Obj.LstIndex];
            unit.transform.position = data.Position.SetPosition();
            unit.transform.localRotation = data.Rotation.SetRotation();
            unit.GetComponent<ObjectInfor>().CurrentHealth = data.Stat.CurrentHealth;
            unit.GetComponent<UnitController>().SetParticle(data.ParticleData.RunTime);
        }

        yield return null;
    }
}