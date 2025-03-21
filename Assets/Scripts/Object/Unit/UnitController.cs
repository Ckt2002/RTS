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

    private ParticleSystem explosion;
    private bool movementPaused;

    private void Start()
    {
        explosion = GetComponentInChildren<ParticleSystem>();
    }

    public void SetParticle(float runTime)
    {
        if (runTime > 0f)
        {
            explosion.Stop();
            explosion.time = runTime;
            explosion.Play();
        }
    }

    protected virtual void Update()
    {
        if (PauseSystem.isPausing)
        {
            movement.Pause();
            movementPaused = true;
            return;
        }

        if (movementPaused) // Chỉ gọi Resume() khi thoát khỏi trạng thái pause
        {
            movement.Resume(); // Tiếp tục di chuyển khi game resume
            movementPaused = false; // Đánh dấu rằng game không còn bị pause
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
        dieStatus.PlaySoundAndParticle(stat);
    }
}