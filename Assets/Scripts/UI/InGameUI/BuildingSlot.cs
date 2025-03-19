using Assets.Scripts.Data;
using UnityEngine;

public class BuildingSlot : ObjectSlot
{
    [SerializeField] private BuildingPooling buildingPooling;
    [SerializeField] private PlaceBuildingSystem placementSystem;

    public override void BuyObject()
    {
        if (resourcesManager.Money < stat.Money)
        {
            Debug.LogWarning("Don't have enough money");
            return;
        }

        if (stat.CompareTag(Tags.PlayerBuilding.ToString()))
        {
            var obj = buildingPooling.GetObjectPool(stat.name);
            if (obj != null)
                placementSystem.GetSelectedBuilding(obj);

            resourcesManager.CurrentMoney(-stat.Money);
        }
    }
}