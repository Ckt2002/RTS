using System.Collections.Generic;

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
}