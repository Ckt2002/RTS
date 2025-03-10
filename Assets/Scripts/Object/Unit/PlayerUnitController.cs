using GameSystem;
using UnityEngine;

public class PlayerUnitController : UnitController
{
    [SerializeField] private PlayerObjectVision vision;

    protected override void Update()
    {
        if (PauseSystem.isPausing) return;

        base.Update();
        vision.SensorEnemy();
    }
}