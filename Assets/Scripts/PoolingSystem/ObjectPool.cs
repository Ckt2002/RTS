using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] protected List<Objects> objectPrefabs;
    [SerializeField] protected List<Transform> parents;

    [HideInInspector] public List<GameObject> objectsSpawned;

    private Dictionary<string, List<GameObject>> objectDictionary;

    protected void Start()
    {
        objectsSpawned = new List<GameObject>();
        objectDictionary = new Dictionary<string, List<GameObject>>();
        StartCoroutine(nameof(SpawnObjects));
    }

    public GameObject GetObjectPool(string objectName)
    {
        if (objectDictionary == null || objectDictionary.Count == 0)
            return null;

        // Debug.Log(objectName);
        // Debug.Log(objectDictionary.TryGetValue(objectName, out var value2));
        if (objectDictionary.TryGetValue(objectName, out var value))
            foreach (var obj in value)
                if (!obj.activeInHierarchy)
                    return obj;

        return null;
    }

    public List<GameObject> GetActiveObjectByName(string objectName)
    {
        if (objectDictionary == null || objectDictionary.Count == 0)
            return null;

        List<GameObject> activeObjects = new();
        if (objectDictionary.TryGetValue(objectName, out var value))
            foreach (var obj in value)
                if (obj.activeInHierarchy)
                    activeObjects.Add(obj);

        return activeObjects;
    }

    public List<GameObject> GetNonActiveObjectByName(string objectName)
    {
        if (objectDictionary == null || objectDictionary.Count == 0)
            return null;

        List<GameObject> nonActiveObjects = new();
        if (objectDictionary.TryGetValue(objectName, out var value))
            foreach (var obj in value)
                if (!obj.activeInHierarchy)
                    nonActiveObjects.Add(obj);

        return nonActiveObjects;
    }

    public List<GameObject> GetAllActiveObject()
    {
        if (objectsSpawned == null || objectsSpawned.Count == 0)
            return null;

        List<GameObject> activeObjects = new();
        foreach (var obj in objectsSpawned)
            if (obj.activeInHierarchy)
                activeObjects.Add(obj);

        return activeObjects;
    }

    protected IEnumerator SpawnObjects()
    {
        foreach (var objectPrefab in objectPrefabs)
        {
            var prefab = objectPrefab.objectPrefab;
            var spawnNumber = objectPrefab.spawnNumber;
            var objectName = prefab.name;
            if (!objectDictionary.ContainsKey(objectName))
                objectDictionary[objectName] = new List<GameObject>();

            var unitList = objectDictionary[objectName];
            var parent = parents[0];
            if (prefab.name.Contains(Names.Enemy))
                parent = parents[1];

            for (var i = 0; i < spawnNumber; i++)
            {
                var go = Instantiate(prefab, parent, true);
                unitList.Add(go);
                objectsSpawned.Add(go);
                go.SetActive(false);
                if (spawnNumber % 300 == 0 && spawnNumber >= 350)
                    yield return new WaitForSeconds(0.0001f);
            }

            yield return new WaitForSeconds(0.0001f);
        }
    }
}