using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    [SerializeField] private List<PlayerUnitStatus> playerUnitsPrefab;

    public List<PlayerUnitStatus> PlayerUnitsPrefab => playerUnitsPrefab;
    public List<UnitController> UnitsSelected { get; } = new();

    private void Awake()
    {
        Instance = this;
    }

    public void AddToSelectedList(UnitController selectedUnit)
    {
        if (selectedUnit != null && !UnitsSelected.Contains(selectedUnit))
        {
            UnitsSelected.Add(selectedUnit);
            var unitRing = selectedUnit.transform.GetComponent<PlayerRing>();
            unitRing.ShowRing();
        }
    }

    public void RemoveFromSelectedList(UnitController selectedUnit)
    {
        if (selectedUnit != null && UnitsSelected.Contains(selectedUnit))
        {
            UnitsSelected.Remove(selectedUnit);
            var unitRing = selectedUnit.transform.GetComponent<PlayerRing>();
            unitRing.HideRing();
        }
    }

    public void CleanSelectedList()
    {
        foreach (var unit in UnitsSelected)
        {
            var unitRing = unit.transform.GetComponent<PlayerRing>();
            unitRing.HideRing();
        }

        UnitsSelected.Clear();
    }
}