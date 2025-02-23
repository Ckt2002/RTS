using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private UnitPooling unitPooling;

    public void SpawnUnit(string unitType)
    {
        var enemy = unitPooling.GetUnit(unitType);

        if (enemy == null)
            return;
    }
}
