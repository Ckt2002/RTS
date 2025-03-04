using UnityEngine;

public class FactoryController : BuildingController
{
    [SerializeField] private Transform rallyPoint;
    [SerializeField] private Transform spawnPoint;

    public Transform RallyPoint => rallyPoint;
    public Transform SpawnPoint => spawnPoint;
}