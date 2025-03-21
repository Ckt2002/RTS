using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] protected List<Objects> objectPrefabs;
    [SerializeField] protected List<Transform> parents;

    [HideInInspector] public List<GameObject> objectsSpawned;

    protected Dictionary<string, List<GameObject>> objectDictionary;
    public Dictionary<string, List<GameObject>> GetObjectDictionary => objectDictionary;

    protected void Start()
    {
        objectsSpawned = new List<GameObject>();
        objectDictionary = new Dictionary<string, List<GameObject>>();
    }

    public List<GameObject> GetObjectByName(string name)
    {
        if (objectDictionary.ContainsKey(name))
            return objectDictionary[name];
        return null;
    }

    public GameObject GetObjectPool(string objectName)
    {
        if (objectDictionary == null || objectDictionary.Count == 0)
        {
            Debug.Log("Dictionary is null");
            return null;
        }

        if (objectDictionary.TryGetValue(objectName, out var value))
            foreach (var obj in value)
                if (!obj.activeInHierarchy)
                    return obj;

        return null;
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

    public void RunSpawnObjects(Action action)
    {
        StartCoroutine(SpawnObjects(action));
    }

    protected virtual IEnumerator SpawnObjects(Action action)
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
            if (prefab.name.Contains(Names.Enemy) || prefab.name.Contains(Names.BuildingParticle))
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

    protected void CallBackAction(Action action)
    {
        action?.Invoke();
    }

    public Dictionary<string, Dictionary<int, GameObject>> GetObjectsToSave()
    {
        var dictionaryToSave = new Dictionary<string, Dictionary<int, GameObject>>();
        foreach (var key in objectDictionary.Keys)
            if (!dictionaryToSave.ContainsKey(key))
                dictionaryToSave.Add(key, new Dictionary<int, GameObject>());

        foreach (var key in dictionaryToSave.Keys)
        foreach (var obj in objectDictionary[key])
        {
            if (!obj.activeInHierarchy) continue;
            dictionaryToSave[key].Add(objectDictionary[key].IndexOf(obj), obj);
        }

        return dictionaryToSave;
    }
}