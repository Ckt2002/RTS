using Assets.Scripts.Data;
using UnityEngine;

public class UnitLightRange : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Tags.EnemyUnit))
        {
            // Debug.Log(col.gameObject.name);
            var unit = col.transform.GetComponent<UnitController>();
            if (unit != null) unit.Visible(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(Tags.EnemyUnit))
        {
            // Debug.Log(col.gameObject.name);
            var unit = col.transform.GetComponent<UnitController>();
            if (unit != null) unit.Visible(false);
        }
    }
}