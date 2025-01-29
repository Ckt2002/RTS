using Assets.Scripts.Data;
using System.Collections.Generic;
using System.Linq;
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

    public List<ObjectInfor> BuildingsOnMap = new();

    public BuildingController buildingSelected { get; private set; }

    public List<GameObject> PlayerBuildingsPrefab => playerBuildingsPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        BuildingsOnMap = FindObjectsOfType<ObjectInfor>()
            .Where(unit => unit.CompareTag(Tags.PlayerBuilding) || unit.CompareTag(Tags.EnemyBuilding))
            .ToList();
        buildingPanel.SetObjectPanel(PlayerBuildingsPrefab);
    }

    public void GetBuildingSelected(BuildingController buildingController)
    {
        buildingSelected = buildingController;
        if (buildingController.transform.name.Contains("Player Factory"))
        {
            panelsManager.ShowUnitPanel();
            var unitsUnlocked = new List<GameObject>();

            foreach (var playerUnitPrefab in unitManager.PlayerUnitsPrefab)
                if (playerUnitPrefab.unlocked && !unitsUnlocked.Contains(playerUnitPrefab.unitPrefab))
                    unitsUnlocked.Add(playerUnitPrefab.unitPrefab);
            unitPanel.SetObjectPanel(unitsUnlocked);
            return;
        }

        if (buildingController.transform.name.Contains("Player Research Lab"))
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

    public void AddNewBuildingOnMap(ObjectInfor newBuilding)
    {
        BuildingsOnMap.Add(newBuilding);
    }
}