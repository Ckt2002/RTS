using System;
using UnityEngine;

[Serializable]
public class Objects
{
    public GameObject objectPrefab;
    public int spawnNumber;
}

[Serializable]
public class PlayerUnitStatus
{
    public GameObject unitPrefab;
    public bool unlocked;
}

[Serializable]
public class PlayerBuildingStatus
{
    public GameObject unitPrefab;
    public int number;
}