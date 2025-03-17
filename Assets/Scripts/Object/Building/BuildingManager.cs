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

    public SelectPanel BuildingPanel => buildingPanel;
    public SelectPanel UnitPanel => unitPanel;
    public SelectPanel ResearchPanel => researchPanel;
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
        SetPanels();
    }

    private void SetPanels()
    {
        SetBuildingPanel();
        SetResearchPanel();
        SetUnitPanel();
        researchPanel.gameObject.SetActive(false);
        unitPanel.gameObject.SetActive(false);
    }

    private void SetBuildingPanel()
    {
        var buildingLst = new List<GameObject>();
        foreach (var building in BuildingPooling.Instance.BuildingPrefabs)
            buildingLst.Add(building.objectPrefab);
        buildingPanel.SetObjectPanel(buildingLst);
    }

    private void SetResearchPanel()
    {
        researchPanel.gameObject.SetActive(true);
        var unitsLocked = new List<GameObject>();

        foreach (var playerUnitPrefab in unitManager.PlayerUnitsPrefab)
            if (!playerUnitPrefab.unlocked && !unitsLocked.Contains(playerUnitPrefab.unitPrefab))
                unitsLocked.Add(playerUnitPrefab.unitPrefab);

        researchPanel.SetObjectPanel(unitsLocked);
    }

    private void SetUnitPanel()
    {
        unitPanel.gameObject.SetActive(true);
        var unitsUnlocked = new List<GameObject>();

        foreach (var playerUnitPrefab in unitManager.PlayerUnitsPrefab)
            if (playerUnitPrefab.unlocked && !unitsUnlocked.Contains(playerUnitPrefab.unitPrefab))
                unitsUnlocked.Add(playerUnitPrefab.unitPrefab);
        unitPanel.SetObjectPanel(unitsUnlocked);
    }

    public void GetBuildingSelected(GameObject building)
    {
        buildingSelected = building.GetComponent<BuildingController>();
        if (building.name.Contains(Names.PlayerFactory))
        {
            panelsManager.ShowUnitPanel();
            SetUnitPanel();
            return;
        }

        if (building.name.Contains(Names.PlayerResearchLab)) panelsManager.ShowResearchPanel();
    }

    public void DeselectBuilding()
    {
        buildingSelected = null;
        panelsManager.ShowBuildingPanel();
    }
}