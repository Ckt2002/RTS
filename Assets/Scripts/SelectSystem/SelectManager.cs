using System.Collections.Generic;
using GameSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    [SerializeField] private DragSelection dragSelection;
    [SerializeField] private ClickSelection clickSelection;
    [SerializeField] private DrawSelectionSquare drawSelectionSquare;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private GraphicRaycaster raycaster;

    private UnitPooling unitPooling;

    private void Start()
    {
        unitPooling = UnitPooling.Instance;
    }

    private void Update()
    {
        if (PauseSystem.isPausing)
        {
            Debug.Log("Paused");
            return;
        }

        var UIObj = GetClickedUIObject();
        ObjectSlot clickedUI = null;

        if (UIObj != null)
        {
            if (UIObj.GetComponent<ObjectSlot>() != null)
                clickedUI = UIObj.GetComponent<ObjectSlot>();
            if (UIObj.GetComponentInParent<ObjectSlot>() != null)
                clickedUI = UIObj.GetComponentInParent<ObjectSlot>();
        }

        if (Input.GetMouseButtonDown(0) && clickedUI == null)
        {
            DeselectAll();
            buildingManager.DeselectBuilding();
            clickSelection.Select(unitManager, buildingManager);
            drawSelectionSquare.ShowSquare();
            dragSelection.StartPos = drawSelectionSquare.startPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && clickedUI == null && !PlaceBuildingSystem.Instance.IsPlacingBuilding)
        {
            dragSelection.EndPos = drawSelectionSquare.endPos = Input.mousePosition;
            drawSelectionSquare.UpdateSelectionBox();
        }

        if (Input.GetMouseButtonUp(0) && clickedUI == null)
        {
            dragSelection.Select(unitPooling, unitManager);
            drawSelectionSquare.ResetBoxSize();
            drawSelectionSquare.HideSquare();
        }
    }

    private void DeselectAll()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            return;
        unitManager.CleanSelectedList();
    }

    private GameObject GetClickedUIObject()
    {
        var pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        if (results.Count > 0)
            return results[0].gameObject;
        return null;
    }
}