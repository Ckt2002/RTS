using Assets.Scripts.Data;
using UnityEngine;

public class BuildingSlot : ObjectSlot
{
    [SerializeField] private BuildingPooling buildingPooling;
    [SerializeField] private PlaceBuildingSystem placementSystem;

    public override void BuyObject()
    {
        base.BuyObject();

        if (stat.CompareTag(Tags.PlayerBuilding.ToString()))
        {
            var obj = buildingPooling.GetObjectPool(stat.name);
            if (obj != null)
                placementSystem.GetSelectedBuilding(obj);

            resourcesManager.CurrentMoney(-stat.Money);
        }
    }
}