using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;

public class BuildingPooling : MonoBehaviour
{
    public static BuildingPooling Instance;
    [SerializeField] private List<Objects> buildingSpawns;
    [SerializeField] private List<Transform> parents;

    private Dictionary<string, List<GameObject>> buildingDictionary;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        buildingDictionary = new Dictionary<string, List<GameObject>>();

        StartCoroutine(SpawnBuildings());
    }

    private IEnumerator SpawnBuildings()
    {
        foreach (var buildingSpawn in buildingSpawns)
        {
            var buildingType = buildingSpawn.buildingPrefab.name;

            if (!buildingDictionary.ContainsKey(buildingType))
                buildingDictionary[buildingType] = new List<GameObject>();

            var buildingList = buildingDictionary[buildingType];
            var parent = parents[0];
            if (!buildingSpawn.buildingPrefab.CompareTag(Tags.PlayerBuilding))
                parent = parents[1];

            for (var i = 0; i < buildingSpawn.spawnNumber; i++)
            {
                var go = Instantiate(buildingSpawn.buildingPrefab);
                go.transform.SetParent(parent);
                buildingList.Add(go);
                go.SetActive(false);
                yield return null;
            }
        }
    }

    public GameObject GetBuilding(string buildingType)
    {
        if (buildingDictionary.ContainsKey(buildingType))
            foreach (var building in buildingDictionary[buildingType])
                if (!building.activeInHierarchy)
                    return building;

        return null;
    }
}