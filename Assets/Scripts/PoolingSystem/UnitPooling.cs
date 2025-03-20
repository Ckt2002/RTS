using System;
using System.Collections;
using UnityEngine;

public class UnitPooling : ObjectPool
{
    public static UnitPooling Instance;

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

    public Transform GetParent(int index)
    {
        return parents[index];
    }
}