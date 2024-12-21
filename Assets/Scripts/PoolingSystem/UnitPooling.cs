using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;

public class UnitPooling : MonoBehaviour
{
    public static UnitPooling Instance;
    [SerializeField] private List<GameObject> unitsPrefab;
    [SerializeField] private int spawnNumber = 15;
    [SerializeField] private List<Transform> parents;

    private Dictionary<string, List<GameObject>> unitDictionary;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        unitDictionary = new Dictionary<string, List<GameObject>>();
        StartCoroutine(SpawnUnits());
    }

    private IEnumerator SpawnUnits()
    {
        foreach (var unitPrefab in unitsPrefab)
        {
            var unitType = unitPrefab.name;
            if (!unitDictionary.ContainsKey(unitType))
                unitDictionary[unitType] = new List<GameObject>();

            var unitList = unitDictionary[unitType];
            var parent = parents[0];
            if (!unitPrefab.CompareTag(Tags.PlayerUnit))
                parent = parents[1];

            for (var i = 0; i < spawnNumber; i++)
            {
                var go = Instantiate(unitPrefab);
                go.transform.SetParent(parent);
                unitList.Add(go);
                go.SetActive(false);

                yield return null;
            }
        }
    }

    public GameObject GetUnit(string unitType)
    {
        if (unitDictionary.ContainsKey(unitType))
            foreach (var unit in unitDictionary[unitType])
                if (!unit.activeInHierarchy)
                    return unit;

        return null;
    }
}