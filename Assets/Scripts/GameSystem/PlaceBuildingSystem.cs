using Assets.Scripts.Data;
using UnityEngine;

public class PlaceBuildingSystem : MonoBehaviour
{
    public static PlaceBuildingSystem Instance;

    [SerializeField] private LayerMask planeLayer;

    private GameObject buildingSelected;

    public bool IsPlacingBuilding { get; private set; }

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
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
        buildingSelected.SetActive(true);
    }

    private void MoveBuildingWithMouse()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && !hit.transform.tag.Equals(Tags.PlayerUnit))
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
            if (buildingCollider.gameObject != buildingSelected && !buildingCollider.CompareTag(Tags.Plane))
            {
                buildingSelected.GetComponent<Collider>().enabled = false;
                Debug.Log("Cannot place building here. It overlaps with another building.");
                return;
            }

        buildingSelected.GetComponent<BuildingController>().SetBuldingOpaque();
        buildingSelected = null;
        IsPlacingBuilding = false;
    }

    private void CancelPlaceBuilding()
    {
        IsPlacingBuilding = false;
        buildingSelected.SetActive(false);
        buildingSelected.GetComponent<Collider>().enabled = true;
        buildingSelected = null;
    }

    private void RotateBuildingWithScrollWheel()
    {
        var scrollData = Input.GetAxis("Mouse ScrollWheel");
        if (scrollData != 0)
            buildingSelected.transform.Rotate(Vector3.up, scrollData * 45);
    }
}