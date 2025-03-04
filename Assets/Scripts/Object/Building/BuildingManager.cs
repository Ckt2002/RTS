using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;
    [SerializeField] private List<GameObject> playerBuildingsPrefab;
    [SerializeField] private PanelsManager panelsManager;
    [SerializeField] private SelectPanel buildingPanel;
    [SerializeField] private SelectPanel unitPanel;
    [SerializeField] private SelectPanel researchPanel;

    [SerializeField] private UnitManager unitManager;

    public BuildingController buildingSelected { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        var buildingLst = new List<GameObject>();
        foreach (var building in BuildingPooling.Instance.BuildingPrefabs) buildingLst.Add(building.objectPrefab);

        buildingPanel.SetObjectPanel(buildingLst);
    }

    public void GetBuildingSelected(BuildingController buildingController)
    {
        buildingSelected = buildingController;
        if (buildingController.transform.name.Contains(Names.PlayerFactory))
        {
            panelsManager.ShowUnitPanel();
            var unitsUnlocked = new List<GameObject>();

            foreach (var playerUnitPrefab in unitManager.PlayerUnitsPrefab)
                if (playerUnitPrefab.unlocked && !unitsUnlocked.Contains(playerUnitPrefab.unitPrefab))
                    unitsUnlocked.Add(playerUnitPrefab.unitPrefab);
            unitPanel.SetObjectPanel(unitsUnlocked);
            return;
        }

        if (buildingController.transform.name.Contains(Names.PlayerResearchLab))
        {
            panelsManager.ShowResearchPanel();
            var unitsLocked = new List<GameObject>();

            foreach (var playerUnitPrefab in unitManager.PlayerUnitsPrefab)
                if (!playerUnitPrefab.unlocked && !unitsLocked.Contains(playerUnitPrefab.unitPrefab))
                    unitsLocked.Add(playerUnitPrefab.unitPrefab);

            researchPanel.SetObjectPanel(unitsLocked);
        }
    }

    public void DeselectBuilding()
    {
        buildingSelected = null;
        panelsManager.ShowBuildingPanel();
    }
}