using Assets.Scripts.Data;
using UnityEngine;

public class DragSelection : MonoBehaviour
{
    [SerializeField] private UnitFormation unitFormation;

    public Vector2 StartPos { get; set; }
    public Vector2 EndPos { get; set; }

    public void Select(UnitPooling unitPooling, UnitManager unitManager)
    {
        if (StartPos == EndPos)
            return;

        Vector2 min = new(Mathf.Min(StartPos.x, EndPos.x), Mathf.Min(StartPos.y, EndPos.y));
        Vector2 max = new(Mathf.Max(StartPos.x, EndPos.x), Mathf.Max(StartPos.y, EndPos.y));

        foreach (var unit in unitPooling.GetAllActiveObject())
        {
            var posToScreen = Camera.main.WorldToScreenPoint(unit.transform.position);

            var isInRange = posToScreen.x < max.x && posToScreen.x > min.x
                                                  && posToScreen.y < max.y && posToScreen.y > min.y;

            if (isInRange && unit.CompareTag(Tags.PlayerUnit.ToString()) &&
                unit.GetComponent<ObjectInfor>().CurrentHealth > 0)
                unitManager.AddToSelectedList(unit.GetComponent<UnitController>());
            else if (!Input.GetKey(KeyCode.LeftControl))
                unitManager.RemoveFromSelectedList(unit.GetComponent<UnitController>());
        }
    }
}