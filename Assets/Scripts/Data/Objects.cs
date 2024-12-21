using System;
using UnityEngine;

[Serializable]
public class Objects
{
    public GameObject buildingPrefab;
    public int spawnNumber;
}

[Serializable]
public class PlayerUnitStatus
{
    public GameObject unitPrefab;
    public bool unlocked;
}