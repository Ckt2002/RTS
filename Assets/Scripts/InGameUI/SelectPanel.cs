using System.Collections.Generic;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    [SerializeField] private List<ObjectSlot> slots;

    public void SetObjectPanel(List<GameObject> objects)
    {
        var index = 0;
        foreach (var objectInList in objects)
        {
            slots[index].SetSlotObject(objectInList);
            index++;
        }

        for (var i = index; i < slots.Count; i++) slots[i].UnableSlot();
    }
}