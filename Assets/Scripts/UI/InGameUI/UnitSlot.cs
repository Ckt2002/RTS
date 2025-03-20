using System.Collections;
using System.Collections.Generic;
using GameSave;
using GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitSlot : ObjectSlot, IPointerClickHandler
{
    [SerializeField] private Image loading;
    [SerializeField] private TMP_Text unitCountText;
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private UnitManager unitManager;

    private readonly Queue<KeyValuePair<string, int>> buildingsQueue = new();
    private Coroutine buyUnitCoroutine;
    private float timer;

    public override void BuyObject()
    {
        if (resourcesManager.Money < stat.Money)
        {
            Debug.LogWarning("Don't have enough money");
            return;
        }


        resourcesManager.Money -= stat.Money;
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

    public void SetBuyUnit(List<ObjectData> buildingsLst, float timer)
    {
        this.timer = timer;
        foreach (var building in buildingsLst)
            buildingsQueue.Enqueue(new KeyValuePair<string, int>(building.Name, building.LstIndex));

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
                yield return new WaitUntil(() => !PauseSystem.isPausing);
            }

            if (buildingsQueue.Count > 0)
            {
                #region Unit create time

                if (UnitPooling.Instance == null) Debug.Log("Pooling is null");

                var unit = UnitPooling.Instance.GetObjectPool(stat.name);
                if (unit is null)
                {
                    buyUnitCoroutine = null;
                    yield break;
                }

                var buyTime = unit.GetComponent<UnitInfor>().BuyTime;
                timer = 0;

                while (timer < buyTime)
                {
                    if (PauseSystem.isPausing)
                    {
                        SaveCreateUnitSystem.SaveBuyUnitPref(stat.name, buildingsQueue, timer);
                        yield return new WaitUntil(() => !PauseSystem.isPausing);
                    }

                    timer += Time.deltaTime;
                    loading.fillAmount = timer / buyTime;
                    yield return null;
                }

                #endregion

                #region Unit spawn position

                var currentBuilding = buildingsQueue.Dequeue();

                var building = BuildingPooling.Instance.BuildingDictionary[currentBuilding.Key][currentBuilding.Value];
                var buildingComponent = building.GetComponent<FactoryController>();
                unit.transform.position = buildingComponent.SpawnPoint.position;
                unit.SetActive(true);
                var rallyPoint = unit.transform.position + new Vector3(0, 0, 20f);
                unit.GetComponent<UnitMovement>().SetTargetPosition(rallyPoint, 0.01f);

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && buildingsQueue.Count > 0)
        {
            CoroutineManager.Instance?.StopCoroutine(buyUnitCoroutine);
            buyUnitCoroutine = null;
            loading.fillAmount = 0;
            buildingsQueue.Dequeue();
            resourcesManager.Money += stat.Money;
            unitCountText.text = buildingsQueue.Count.ToString();

            if (buildingsQueue.Count > 0)
                buyUnitCoroutine ??= CoroutineManager.Instance?.StartManagedCoroutine(CreateUnitsCoroutine());
        }
    }
}