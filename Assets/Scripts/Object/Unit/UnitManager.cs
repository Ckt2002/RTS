using Assets.Scripts.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    [SerializeField] private List<PlayerUnitStatus> playerUnitsPrefab;
    public List<ObjectInfor> UnitsOnMap = new();
    public List<UnitController> UnitsSelected { get; } = new();

    public List<PlayerUnitStatus> PlayerUnitsPrefab => playerUnitsPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UnitsOnMap = FindObjectsOfType<ObjectInfor>()
            .Where(unit => unit.CompareTag(Tags.PlayerUnit) || unit.CompareTag(Tags.EnemyUnit))
            .ToList();
    }

    public void AddNewUnitOnMap(ObjectInfor newUnit)
    {
        UnitsOnMap.Add(newUnit);
    }

    public void AddToSelectedList(UnitController selectedUnit)
    {
        if (selectedUnit != null && !UnitsSelected.Contains(selectedUnit))
        {
            UnitsSelected.Add(selectedUnit);
            var unitRing = selectedUnit.transform.GetComponent<PlayerRing>();
            unitRing.UnitSelected();
        }
    }

    public void RemoveFromSelectedList(UnitController selectedUnit)
    {
        if (selectedUnit != null && UnitsSelected.Contains(selectedUnit))
        {
            UnitsSelected.Remove(selectedUnit);
            var unitRing = selectedUnit.transform.GetComponent<PlayerRing>();
            unitRing.UnitDeselected();
        }
    }

    public void CleanSelectedList()
    {
        foreach (var unit in UnitsSelected)
        {
            var unitRing = unit.transform.GetComponent<PlayerRing>();
            unitRing.UnitDeselected();
        }

        UnitsSelected.Clear();
    }
}