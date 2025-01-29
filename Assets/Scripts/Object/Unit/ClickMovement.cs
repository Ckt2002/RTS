using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    [SerializeField] private UnitFormation unitFormation;
    [SerializeField] private LayerMask planeLayer;
    private RaycastHit hit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, planeLayer))
            {
                unitFormation.GetInFormationAndMove(hit.point);
                Debug.Log("Clicked Pos: " + hit.point);
            }
        }
    }
}