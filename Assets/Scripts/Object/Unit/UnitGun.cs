using System.Collections.Generic;
using UnityEngine;

public class UnitGun : MonoBehaviour
{
    [SerializeField] private List<Transform> firePoints;

    [SerializeField] private UnitInfor unitStat;

    [SerializeField] private ObjectSound sound;

    private BulletPooling bulletPooling;

    private float fireCooldown;

    private void Start()
    {
        bulletPooling = BulletPooling.Instance;
    }

    public void ResetCoolDown()
    {
        fireCooldown = 0f;
    }

    public void CoolDownCalculator()
    {
        fireCooldown += Time.deltaTime;
    }

    public void Fire()
    {
        if (fireCooldown >= 1 / unitStat.FireRate)
        {
            foreach (var firePoint in firePoints)
            {
                var bulletName = transform.name.Contains(Names.Player)
                    ? Names.PlayerGroundBullet
                    : Names.EnemyGroundBullet;
                var bullet = bulletPooling.GetObjectPool(bulletName);
                if (bullet == null)
                {
                    Debug.LogWarning("Can't find available bullet");
                    return;
                }

                bullet.GetComponent<UnitBullet>().SetupDamage(unitStat.Damage);
                bullet.transform.position = firePoint.transform.position;
                bullet.transform.rotation = firePoint.transform.rotation;
                bullet.SetActive(true);
            }

            fireCooldown = 0f;
            if (sound != null)
                sound.PlayAttackSound();
        }
    }
}