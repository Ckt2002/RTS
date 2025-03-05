using System;
using System.Collections;

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
}