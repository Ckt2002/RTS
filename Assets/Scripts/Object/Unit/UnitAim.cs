using UnityEngine;

public class UnitAim : MonoBehaviour
{
    [SerializeField] private UnitCombat unitCombat;
    [SerializeField] private Transform turret;
    [SerializeField] private UnitGun unitGun;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float aimThreshold = 0.1f;

    public void RotateGun(ObjectInfor target)
    {
        var directionToTarget = transform.forward;
        if (target != null)
            directionToTarget = target.transform.position - transform.position;

        directionToTarget.y = 0;
        var targetRotation = Quaternion.LookRotation(directionToTarget);
        turret.rotation = Quaternion.RotateTowards(turret.rotation,
            targetRotation, rotationSpeed * Time.deltaTime);

        AimFire(targetRotation, target);
    }

    private void AimFire(Quaternion targetRotation, ObjectInfor target)
    {
        if (Quaternion.Angle(turret.rotation, targetRotation) < aimThreshold
            && target != null)
            unitGun.Fire();
    }
}