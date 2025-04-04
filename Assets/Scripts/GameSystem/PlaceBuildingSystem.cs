﻿using Assets.Scripts.Data;
using GameSystem;
using UnityEngine;
using UnityEngine.AI;

public class PlaceBuildingSystem : MonoBehaviour
{
    public static PlaceBuildingSystem Instance;

    [SerializeField] private LayerMask planeLayer;
    [SerializeField] private BuildingManager buildingsManager;

    private GameObject buildingSelected;

    public bool IsPlacingBuilding { get; private set; }

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (PauseSystem.isPausing) return;

        if (IsPlacingBuilding && buildingSelected != null)
        {
            MoveBuildingWithMouse();

            if (Input.GetMouseButtonDown(0))
                PlaceBuilding();
            RotateBuildingWithScrollWheel();
            if (Input.GetMouseButtonDown(1))
                CancelPlaceBuilding();
        }
    }

    public void GetSelectedBuilding(GameObject building)
    {
        IsPlacingBuilding = true;
        buildingSelected = building;
        buildingSelected.transform.rotation = Quaternion.identity;
        buildingSelected.GetComponent<BuildingController>().SetBuildingTransparent();
        buildingSelected.GetComponent<Collider>().enabled = false;
        buildingSelected.GetComponent<FogOfWarUnit>().enabled = false;
        buildingSelected.GetComponent<NavMeshObstacle>().enabled = false;
        buildingSelected.tag = nameof(Tags.Unable);
        buildingSelected.SetActive(true);
    }

    private void MoveBuildingWithMouse()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, planeLayer))
            buildingSelected.transform.position = hit.point;
    }

    private void PlaceBuilding()
    {
        buildingSelected.GetComponent<Collider>().enabled = true;
        var buildingsCollider = Physics.OverlapBox(
            buildingSelected.transform.position,
            buildingSelected.GetComponent<Collider>().bounds.extents,
            buildingSelected.transform.rotation
        );

        foreach (var buildingCollider in buildingsCollider)
            if (buildingCollider.gameObject != buildingSelected && !buildingCollider.CompareTag(Tags.Plane.ToString()))
            {
                buildingSelected.GetComponent<Collider>().enabled = false;
                Debug.LogWarning("Cannot place building here. It overlaps with another building.");
                return;
            }

        buildingSelected.GetComponent<BuildingController>().SetBuildingOpaque();
        buildingSelected.GetComponent<NavMeshObstacle>().enabled = true;
        buildingSelected = null;
        IsPlacingBuilding = false;
    }

    private void CancelPlaceBuilding()
    {
        IsPlacingBuilding = false;
        buildingSelected.SetActive(false);
        buildingSelected.GetComponent<Collider>().enabled = true;
        buildingSelected.GetComponent<NavMeshObstacle>().enabled = true;
        buildingSelected.GetComponent<FogOfWarUnit>().enabled = true;
        buildingSelected.tag = nameof(Tags.PlayerBuilding);
        buildingSelected = null;
    }

    private void RotateBuildingWithScrollWheel()
    {
        var scrollData = Input.GetAxis("Mouse ScrollWheel");
        if (scrollData != 0)
            buildingSelected.transform.Rotate(Vector3.up, scrollData * 45);
    }
}