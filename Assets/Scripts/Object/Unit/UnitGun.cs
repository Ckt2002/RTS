using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;

public class UnitGun : MonoBehaviour
{
    [SerializeField] private List<Transform> firePoints;
    [SerializeField] private UnitInfor unitStat;
    [SerializeField] private int ammo;

    private BulletPooling bulletPooling;

    private float fireCooldown;

    private void Start()
    {
        bulletPooling = BulletPooling.instance;
        var groundUnit = gameObject.name.Contains("Aircraft") || gameObject.name.Contains("Anti Air");
        bulletPooling.CreateBullets(CompareTag(Tags.PlayerUnit), groundUnit, ammo);
    }

    private void Update()
    {
        fireCooldown += Time.deltaTime;
    }

    public void Fire()
    {
        if (fireCooldown > 1 / unitStat.FireRate)
        {
            foreach (var firePoint in firePoints)
            {
                var groundUnit = gameObject.name.Contains("Aircraft") || gameObject.name.Contains("Anti Air");
                var bullet = bulletPooling.GetBullet(CompareTag(Tags.PlayerUnit), groundUnit);
                if (bullet == null)
                    return;
                bullet.GetComponent<UnitBullet>().SetupDamage(unitStat.Damage);
                bullet.transform.position = firePoint.transform.position;
                bullet.transform.rotation = firePoint.transform.rotation;
                bullet.SetActive(true);
            }

            fireCooldown = 0f;
        }
    }
}