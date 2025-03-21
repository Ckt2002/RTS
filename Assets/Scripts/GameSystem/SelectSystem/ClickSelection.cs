using UnityEngine;

public class ClickSelection : MonoBehaviour
{
    [SerializeField] private LayerMask unitLayer;
    [SerializeField] private LayerMask buildingLayer;

    private RaycastHit hit;

    public void Select(UnitManager unitManager, BuildingManager buildingManager)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, unitLayer))
            unitManager.AddToSelectedList(hit.transform.GetComponent<UnitController>());
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingLayer))
            buildingManager.GetBuildingSelected(hit.transform.gameObject);
    }
}