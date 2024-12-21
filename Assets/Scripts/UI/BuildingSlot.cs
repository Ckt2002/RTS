using Assets.Scripts.Data;
using UnityEngine;

public class BuildingSlot : ObjectSlot
{
    [SerializeField] private BuildingPooling buildingPooling;
    [SerializeField] private PlaceBuildingSystem placementSystem;

    public override void BuyObject()
    {
        if (stat.CompareTag(Tags.PlayerBuilding))
        {
            var obj = buildingPooling.GetBuilding(stat.name);
            if (obj != null)
                placementSystem.GetSelectedBuilding(obj);

            resourcesManager.CurrentMoney(-stat.Money);
        }
    }
}