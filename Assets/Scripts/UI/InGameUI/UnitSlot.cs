using System.Collections;
using System.Collections.Generic;
using GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSlot : ObjectSlot
{
    [SerializeField] private Image loading;
    [SerializeField] private TMP_Text unitCountText;
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private UnitManager unitManager;

    private readonly Queue<KeyValuePair<string, int>> buildingsQueue = new();
    private Coroutine buyUnitCoroutine;
    private UnitPooling unitPooling;

    private void Start()
    {
        unitPooling = UnitPooling.Instance;
    }

    public override void BuyObject()
    {
        base.BuyObject();
        var selectedBuilding = buildingManager.buildingSelected;

        var buildingName = selectedBuilding.name.Replace("(Clone)", "");
        var buildingIndex = GetBuildingIndexFromPooling(buildingName, selectedBuilding);
        if (buildingIndex < 0)
        {
            Debug.LogError("Something happened with name sub");
            return;
        }

        buildingsQueue.Enqueue(new KeyValuePair<string, int>(buildingName, buildingIndex));

        unitCountText.text = buildingsQueue.Count.ToString();

        buyUnitCoroutine ??= CoroutineManager.Instance?.StartManagedCoroutine(CreateUnitsCoroutine());
    }

    private IEnumerator CreateUnitsCoroutine()
    {
        while (buildingsQueue.Count > 0)
        {
            if (PauseSystem.isPausing)
            {
                SaveCreateUnitSystem.SaveBuyUnitPref(stat.name, buildingsQueue, 0f);
                yield return null;
                continue;
            }

            if (buildingsQueue.Count > 0)
            {
                #region Unit create time

                var unit = unitPooling.GetObjectPool(stat.name);
                if (unit is null)
                {
                    buyUnitCoroutine = null;
                    yield break;
                }

                var buyTime = unit.GetComponent<UnitInfor>().BuyTime;
                var elapsedTime = 0f;

                while (elapsedTime < buyTime)
                {
                    if (PauseSystem.isPausing)
                    {
                        SaveCreateUnitSystem.SaveBuyUnitPref(stat.name, buildingsQueue, elapsedTime);
                        yield return null;
                        continue;
                    }

                    // elapsedTime += Time.deltaTime;
                    loading.fillAmount = elapsedTime / buyTime;
                    yield return null;
                }

                #endregion


                #region Unit spawn position

                var currentBuilding = buildingsQueue.Dequeue();

                var building = BuildingPooling.Instance.BuildingDictionary[currentBuilding.Key][currentBuilding.Value];
                unit.transform.position = building.GetComponent<FactoryController>().SpawnPoint.position;
                unit.SetActive(true);

                unitCountText.text = buildingsQueue.Count.ToString();

                #endregion
            }
        }

        loading.fillAmount = 0f;
        unitCountText.text = "";
        buyUnitCoroutine = null;
        SaveCreateUnitSystem.ClearBuyUnitPref(stat.name);
    }

    private int GetBuildingIndexFromPooling(string name, BuildingController buildingController)
    {
        var buildingName = name.Replace("(Clone", "");
        var buildingLst = BuildingPooling.Instance.GetObjectByName(buildingName);
        foreach (var building in buildingLst)
            if (buildingController.gameObject == building)
                return buildingLst.IndexOf(building);

        return -1;
    }
}