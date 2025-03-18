using System.Collections.Generic;
using UnityEngine;

public class SaveLoadGameUI : MonoBehaviour
{
    [SerializeField] private GameObject loadGameSlotButton;
    [SerializeField] private Transform content;
    [SerializeField] private bool isSave;

    private readonly List<GameObject> slotPool = new();

    private void OnEnable()
    {
        SaveLoadSystem.Instance.OnSlotsUpdated += UpdateUI;
        UpdateUI();
    }

    private void OnDisable()
    {
        SaveLoadSystem.Instance.OnSlotsUpdated -= UpdateUI;
    }

    private void UpdateUI()
    {
        ClearSlots();

        foreach (var slotData in SaveLoadSystem.Instance.LoadGameSlots)
        {
            GameObject slot;
            if (slotPool.Count > 0)
            {
                slot = slotPool[0];
                slotPool.RemoveAt(0);
            }
            else
            {
                slot = Instantiate(loadGameSlotButton, content);
            }

            slot.SetActive(true);
            var slotComponent = slot.GetComponent<LoadGameSlot>();
            slotComponent.SetSlotInfor(slotData.Date, slotData.Name, isSave, slotData.IsCloud);
        }
    }

    private void ClearSlots()
    {
        foreach (Transform child in content)
        {
            child.gameObject.SetActive(false);
            slotPool.Add(child.gameObject);
        }
    }
}