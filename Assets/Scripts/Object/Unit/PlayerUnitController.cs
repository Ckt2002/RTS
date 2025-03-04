using UnityEngine;

public class PlayerUnitController : UnitController
{
    [SerializeField] private PlayerObjectVision vision;

    protected override void Update()
    {
        base.Update();
        vision.SensorEnemy();
    }
}