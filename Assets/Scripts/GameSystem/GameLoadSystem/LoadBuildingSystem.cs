using System.Collections.Generic;
using System.Threading.Tasks;
using GameSave;

public class LoadBuildingSystem
{
    public static Task LoadBuilding(List<BuildingData> buildingDatas)
    {
        if (buildingDatas == null) return Task.CompletedTask;

        var buildingDictionary = BuildingPooling.Instance.GetObjectDictionary;
        foreach (var data in buildingDatas)
        {
            var building = buildingDictionary[data.Obj.Name][data.Obj.LstIndex];
            building.GetComponent<ObjectInfor>().CurrentHealth = data.Stat.CurrentHealth;
            building.transform.position = data.Position.SetPosition();
            building.transform.localRotation = data.Rotation.SetRotation();
            if (data.DieData != null) building.GetComponent<ObjectDieStatus>().LoadProgress(data.DieData);
            building.SetActive(true);
        }

        return Task.CompletedTask;
    }
}