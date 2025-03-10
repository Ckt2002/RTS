using System.Collections.Generic;
using System.Linq;
using GameSave;
using UnityEngine;

public class SaveResearchSystem : MonoBehaviour
{
    private static readonly List<ResearchData> researchDatas = new();

    public static List<ResearchData> SaveResearchDatas()
    {
        return researchDatas;
    }

    public static void SaveResearchPref(string unitName, float elapsedTime)
    {
        if (researchDatas.All(data => data.unitName != unitName))
            researchDatas.Add(new ResearchData
            {
                elapsedTime = elapsedTime,
                unitName = unitName
            });
    }

    public static void ClearBuyUnitPref(string unitName)
    {
        PlayerPrefs.DeleteKey(PlayerPrefsName.ResearchProgress + unitName);
        PlayerPrefs.DeleteKey(PlayerPrefsName.UnitResearch + unitName);
        PlayerPrefs.Save();
    }
}