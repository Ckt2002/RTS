using System.Collections;
using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class LoadBuildingSystem : MonoBehaviour
{
    public static IEnumerator LoadBuilding(List<BuildingData> buildingDatas)
    {
        var buildingDictionary = BuildingPooling.Instance.GetObjectDictionary;
        foreach (var data in buildingDatas)
        {
            var building = buildingDictionary[data.Obj.Name][data.Obj.LstIndex];
            building.GetComponent<ObjectInfor>().CurrentHealth = data.Stat.CurrentHealth;
            building.transform.position = data.Position.SetPosition();
            building.transform.localRotation = data.Rotation.SetRotation();
            building.GetComponent<BuildingController>().SetParticle(data.ParticleData.RunTime);
            building.SetActive(true);
        }

        yield return null;
    }
}