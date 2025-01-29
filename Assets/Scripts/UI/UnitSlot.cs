using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSlot : ObjectSlot
{
    [SerializeField] private Image loading;
    [SerializeField] private TMP_Text unitCountText;
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private UnitPooling unitPooling;
    [SerializeField] private UnitManager unitManager;

    private readonly Queue<BuildingController> buildingsQueue = new();
    private readonly Dictionary<BuildingController, int> unitCountPerBuilding = new();
    private Coroutine buyUnitCoroutine;
    private int totalUnitNumber;

    public override void BuyObject()
    {
        var selectedBuilding = buildingManager.buildingSelected;

        if (unitCountPerBuilding.TryAdd(selectedBuilding, 0))
            buildingsQueue.Enqueue(selectedBuilding);

        totalUnitNumber++;
        unitCountPerBuilding[selectedBuilding]++;
        unitCountText.text = totalUnitNumber.ToString();

        buyUnitCoroutine ??= CoroutineManager.Instance.StartManagedCoroutine(CreateUnitsCoroutine());
    }

    private IEnumerator CreateUnitsCoroutine()
    {
        while (totalUnitNumber > 0)
            if (buildingsQueue.Count > 0)
            {
                var currentBuilding = buildingsQueue.Dequeue();
                buildingsQueue.Enqueue(currentBuilding);

                if (unitCountPerBuilding[currentBuilding] <= 0)
                    continue;

                #region Unit create time

                var unit = unitPooling.GetUnit(stat.name);
                if (unit is null)
                {
                    buyUnitCoroutine = null;
                    yield break;
                }

                var buyTime = unit.GetComponent<UnitInfor>().BuyTime;
                var elapsedTime = 0f;

                while (elapsedTime < buyTime)
                {
                    elapsedTime += Time.deltaTime;
                    loading.fillAmount = elapsedTime / buyTime;
                    yield return null;
                }

                #endregion

                #region Unit spawn position

                var transform1 = currentBuilding.transform;
                var spawnPosition = transform1.position;
                var spawnRotation = transform1.rotation;
                unit.transform.position = spawnPosition;
                unit.transform.rotation = spawnRotation;
                unit.SetActive(true);
                unit.GetComponent<UnitMovement>()
                    .Move(currentBuilding.GetComponent<FactoryController>().RallyPoint.position, 0f);
                unitManager.AddNewUnitOnMap(unit.GetComponent<ObjectInfor>());

                #endregion

                unitCountPerBuilding[currentBuilding]--;
                totalUnitNumber--;
                unitCountText.text = totalUnitNumber.ToString();
            }

        loading.fillAmount = 0f;
        unitCountText.text = "";
        buyUnitCoroutine = null;
    }
}