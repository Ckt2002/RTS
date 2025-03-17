using System.Collections;
using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class LoadResearchSystem
{
    public static IEnumerator LoadResearch(List<ResearchData> researchDatas)
    {
        var researchSlots = BuildingManager.Instance.ResearchPanel.Slots;

        var slotDictionary = new Dictionary<string, ResearchSlot>();
        foreach (var slot in researchSlots)
            if (slot is ResearchSlot researchSlot)
            {
                if (researchSlot.Stat == null)
                    continue;
                var statName = researchSlot.Stat.gameObject.name;
                if (!slotDictionary.ContainsKey(statName)) slotDictionary[statName] = researchSlot;
            }

        foreach (var data in researchDatas)
            if (slotDictionary.TryGetValue(data.unitName, out var researchSlot))
            {
                // Debug.Log($"Found matching ResearchSlot for {data.unitName}");
                if (!data.researchCompleted)
                    researchSlot.SetResearchCoroutine(data.elapsedTime);
                else
                    researchSlot.SetCompleted();
            }
            else
            {
                Debug.LogWarning($"No matching ResearchSlot found for {data.unitName}");
            }


        yield return null;
    }
}