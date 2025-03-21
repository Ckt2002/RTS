using System;
using System.Collections;

public class BulletPooling : ObjectPool
{
    public static BulletPooling Instance;

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