using Assets.Scripts.Data;
using GameSystem;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    [SerializeField] private UnitFormation unitFormation;
    [SerializeField] private LayerMask planeLayer;
    [SerializeField] private LayerMask enemyLayer;
    private RaycastHit hit;

    private void Update()
    {
        if (PauseSystem.isPausing) return;

        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, planeLayer))
                unitFormation.GetInFormationAndMove(hit.point);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
                if (hit.transform.CompareTag(Tags.EnemyUnit.ToString()) ||
                    hit.transform.CompareTag(Tags.EnemyBuilding.ToString()))
                {
                    Debug.Log(hit.transform.name);
                    unitFormation.GetInAttackStatusAndMove(hit.point);
                }
        }
    }
}