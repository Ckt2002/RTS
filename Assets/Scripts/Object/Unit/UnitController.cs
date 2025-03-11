using GameSystem;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] protected UnitInfor stat;
    [SerializeField] protected UnitMovement movement;
    [SerializeField] protected UnitCombat combat;
    [SerializeField] protected UnitAim aiming;
    [SerializeField] protected UnitGun gun;
    [SerializeField] protected ObjectDieStatus dieStatus;

    private ParticleSystem exposion;

    private void Start()
    {
        exposion = GetComponentInChildren<ParticleSystem>();
    }

    public void SetParticle(float runTime)
    {
        if (runTime > 0f)
        {
            exposion.Stop();
            exposion.time = runTime;
            exposion.Play();
        }
    }

    protected virtual void Update()
    {
        if (PauseSystem.isPausing) return;

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