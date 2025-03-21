using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class SaveCreateUnitSystem
{
    private static readonly HashSet<string> unitNames = new();

    public static List<BuyUnitData> SaveCreateUnit()
    {
        var buyUnitDatas = new List<BuyUnitData>();
        foreach (var unitName in unitNames)
        {
            var json = PlayerPrefs.GetString(PlayerPrefsName.BuildingQueue + unitName);
            var wrapper = JsonUtility.FromJson<BuildingQueueJSONList>(json);

            var data = new BuyUnitData
            {
                unitName = unitName,
                buyTime = PlayerPrefs.GetFloat(PlayerPrefsName.UnitBuyTime + unitName, 0f),
                buildingQueueLst = new List<ObjectData>()
            };
            data.buildingQueueLst.AddRange(wrapper.list);
            buyUnitDatas.Add(data);
            ClearBuyUnitPref(unitName);
        }

        return buyUnitDatas;
    }

    public static void SaveBuyUnitPref(string unitName, Queue<KeyValuePair<string, int>> buildingsQueue,
        float elapsedTime)
    {
        unitNames.Add(unitName);
        PlayerPrefs.SetString(PlayerPrefsName.UnitName + unitName, unitName);
        PlayerPrefs.SetFloat(PlayerPrefsName.UnitBuyTime + unitName, elapsedTime);

        var list = new List<ObjectData>();
        foreach (var item in buildingsQueue)
            list.Add(new ObjectData { Name = item.Key, LstIndex = item.Value });

        var wrapper = new BuildingQueueJSONList { list = list };
        var json = JsonUtility.ToJson(wrapper, true);

        PlayerPrefs.SetString(PlayerPrefsName.BuildingQueue + unitName, json);
        PlayerPrefs.Save();
    }

    public static void ClearBuyUnitPref(string unitName)
    {
        PlayerPrefs.DeleteKey(PlayerPrefsName.TotalUnitBuy + unitName);
        PlayerPrefs.DeleteKey(PlayerPrefsName.UnitBuyTime + unitName);
        PlayerPrefs.DeleteKey(PlayerPrefsName.UnitName + unitName);
        PlayerPrefs.DeleteKey(PlayerPrefsName.BuildingQueue + unitName);
        PlayerPrefs.Save();
    }
}