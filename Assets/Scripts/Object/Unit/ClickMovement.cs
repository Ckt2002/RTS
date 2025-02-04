using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    [SerializeField] private UnitFormation unitFormation;
    [SerializeField] private LayerMask planeLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private List<string> enemyTags;
    private RaycastHit hit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, planeLayer))
            {
                unitFormation.GetInFormationAndMove(hit.point);
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
            {
                //if (hit.transform.CompareTag(Tags.EnemyUnit) || hit.transform.CompareTag(Tags.EnemyBuilding))
                //{
                Debug.Log(hit.transform.name);
                unitFormation.GetInAttackStatusAndMove(hit.point);
                //}
            }
        }
    }
}