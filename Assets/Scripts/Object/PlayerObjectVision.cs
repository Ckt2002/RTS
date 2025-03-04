using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using UnityEngine;

public class PlayerObjectVision : MonoBehaviour
{
    [SerializeField] private ObjectInfor stat;

    private readonly Dictionary<GameObject, bool> enemyVisibilityStatus = new();

    private BuildingPooling buildingPooling;
    private UnitPooling unitPooling;

    private void Start()
    {
        unitPooling = UnitPooling.Instance;
        buildingPooling = BuildingPooling.Instance;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stat.Vision);
    }

    public void SensorEnemy()
    {
        foreach (var obj in CombineUnits())
            if (obj.CompareTag(Tags.EnemyUnit.ToString()) || obj.CompareTag(Tags.EnemyBuilding.ToString()))
            {
                var visibleComponent = obj.GetComponent<EnemyVisible>();
                var isInVision = Vector3.Distance(transform.position, obj.transform.position) <= stat.Vision;

                if (!enemyVisibilityStatus.ContainsKey(obj))
                {
                    if (isInVision)
                    {
                        visibleComponent.NewCombined();
                        enemyVisibilityStatus.Add(obj, true);
                    }
                }
                else
                {
                    if (!isInVision)
                    {
                        visibleComponent.NewUncombined();
                        enemyVisibilityStatus.Remove(obj);
                    }
                }

                visibleComponent.CheckInVision();
            }
    }

    private List<GameObject> CombineUnits()
    {
        return unitPooling.GetAllActiveObject()
            .Concat(buildingPooling.GetAllActiveObject())
            .ToList();
    }
}