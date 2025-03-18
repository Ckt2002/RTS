using System.Collections.Generic;
using System.Threading.Tasks;
using GameSave;

public class LoadUnitSystem
{
    public static Task LoadUnit(List<UnitData> unitDatas)
    {
        if (unitDatas == null) return Task.CompletedTask;

        var unitDictionary = UnitPooling.Instance.GetObjectDictionary;
        foreach (var data in unitDatas)
        {
            var unit = unitDictionary[data.Obj.Name][data.Obj.LstIndex];
            unit.transform.position = data.Position.SetPosition();
            unit.transform.localRotation = data.Rotation.SetRotation();
            unit.GetComponent<ObjectInfor>().CurrentHealth = data.Stat.CurrentHealth;
            unit.GetComponent<UnitController>().SetParticle(data.ParticleData.RunTime);
            unit.SetActive(true);
        }

        return Task.CompletedTask;
    }
}