using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPooling : ObjectPool
{
    public static BuildingPooling Instance;

    public List<Objects> BuildingPrefabs => objectPrefabs;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    protected override IEnumerator SpawnObjects(Action action)
    {
        yield return base.SpawnObjects(action);

        CallBackAction(action);
    }

    public void CreateHQ()
    {
        var HQ = GetObjectPool(Names.PlayerHeadquarters);
        if (HQ != null)
        {
            HQ.SetActive(true);
            HQ.transform.position = Vector3.up;
        }
    }
}