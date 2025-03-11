using System.Collections;
using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class LoadBuildingSystem : MonoBehaviour
{
    public static IEnumerator LoadBuilding(List<BuildingData> buildingDatas)
    {
        foreach (var data in buildingDatas) Debug.Log(data.Obj.Name);
        yield return null;
    }
}