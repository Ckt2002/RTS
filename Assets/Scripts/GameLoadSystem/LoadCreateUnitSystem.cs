using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCreateUnitSystem
{
    public static IEnumerator LoadCreateUnitProgress(List<BuyUnitData> buyUnitDatas)
    {
        var unitSlots = BuildingManager.Instance.UnitPanel.Slots;

        var slotDictionary = new Dictionary<string, UnitSlot>();
        foreach (var slot in unitSlots)
            if (slot is UnitSlot unitSlot)
            {
                if (unitSlot.Stat == null)
                    continue;
                var statName = unitSlot.Stat.gameObject.name;
                if (!slotDictionary.ContainsKey(statName)) slotDictionary[statName] = unitSlot;
            }

        foreach (var data in buyUnitDatas)
            if (slotDictionary.TryGetValue(data.unitName, out var unitSlot))
            {
                if (buyUnitDatas.Count > 0) unitSlot.SetBuyUnit(data.buildingQueueLst, data.buyTime);
            }
            else
            {
                Debug.LogWarning($"No matching ResearchSlot found for {data.unitName}");
            }

        yield return null;
    }
}