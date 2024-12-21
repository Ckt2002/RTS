using UnityEngine;

public class FactoryController : BuildingController
{
    [SerializeField] private Transform rallyPoint;

    public Transform RallyPoint => rallyPoint;
}