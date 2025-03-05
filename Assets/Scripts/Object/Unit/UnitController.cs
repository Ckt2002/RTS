﻿using GameSystem;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] protected UnitInfor stat;
    [SerializeField] protected UnitMovement movement;
    [SerializeField] protected UnitCombat combat;
    [SerializeField] protected UnitAim aiming;
    [SerializeField] protected UnitGun gun;
    [SerializeField] protected ObjectDieStatus dieStatus;

    protected virtual void Update()
    {
        if (PauseSystem.isPausing)
        {
            Debug.Log("Paused");
            return;
        }

        if (!stat.IsAlive())
        {
            DieStatusCalculator();
            gun.ResetCoolDown();
            return;
        }

        MovementCalculator();
        CombatCalculator();
    }

    protected virtual void MovementCalculator()
    {
        movement.Move();
    }

    protected virtual void CombatCalculator()
    {
        aiming.RotateGun(combat.FindTargetInRange());
        gun.CoolDownCalculator();
    }

    protected virtual void DieStatusCalculator()
    {
        dieStatus.PlaySoundAndParticle();
        dieStatus.DarkRenderer();
        dieStatus.ResetCalculator();
        dieStatus.ResetStatus(stat);
    }
}