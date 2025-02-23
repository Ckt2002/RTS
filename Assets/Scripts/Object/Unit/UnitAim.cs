using UnityEngine;

public class UnitAim : MonoBehaviour
{
    [SerializeField] private UnitCombat unitCombat;
    [SerializeField] private UnitController unitController;
    [SerializeField] private Transform turret;
    [SerializeField] private UnitGun unitGun;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float aimThreshold = 0.1f;

    private void Update()
    {
        if (!unitController.IsAlive())
        {
            return;
        }

        Vector3 directionToTarget = transform.forward;
        if (unitCombat.target != null)
            directionToTarget = unitCombat.target.transform.position - transform.position;

        directionToTarget.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        turret.rotation = Quaternion.RotateTowards(turret.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Fire(targetRotation);
    }

    private void Fire(Quaternion targetRotation)
    {
        if (Quaternion.Angle(turret.rotation, targetRotation) < aimThreshold
            && unitCombat.target != null)
            unitGun.Fire();
    }
}
