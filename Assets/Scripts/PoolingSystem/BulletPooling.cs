using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public static BulletPooling instance;

    [SerializeField] private List<GameObject> bulletPrefabs;
    [SerializeField] private List<Transform> parents;
    [SerializeField] private List<GameObject> playerGroundBullets;
    [SerializeField] private List<GameObject> playerAirBullets;
    [SerializeField] private List<GameObject> enemyGroundBullets;
    [SerializeField] private List<GameObject> enemyAirBullets;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        playerGroundBullets = new List<GameObject>();
        playerAirBullets = new List<GameObject>();
        enemyGroundBullets = new List<GameObject>();
        enemyAirBullets = new List<GameObject>();
    }

    public void CreateBullets(bool isPlayer, bool isAirUnit, int ammo)
    {
        var spawnParent = parents[0];
        var prefabList = playerGroundBullets;
        var bulletPrefab = bulletPrefabs[0];

        if (isPlayer && isAirUnit)
        {
            spawnParent = parents[1];
            prefabList = playerAirBullets;
            bulletPrefab = bulletPrefabs[1];
        }

        if (!isPlayer && !isAirUnit)
        {
            spawnParent = parents[2];
            prefabList = enemyGroundBullets;
            bulletPrefab = bulletPrefabs[0];
        }

        if (!isPlayer && isAirUnit)
        {
            spawnParent = parents[3];
            prefabList = enemyAirBullets;
            bulletPrefab = bulletPrefabs[1];
        }

        for (var i = 0; i < ammo; i++)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.parent = spawnParent;
            bullet.GetComponent<UnitBullet>().BulletOwner(isPlayer);
            bullet.SetActive(false);
            prefabList.Add(bullet);
        }
    }

    public GameObject GetBullet(bool isPlayer, bool isAirUnit)
    {
        var bulletList = playerGroundBullets;

        if (isPlayer && isAirUnit) bulletList = playerAirBullets;

        if (!isPlayer && !isAirUnit) bulletList = enemyGroundBullets;

        if (!isPlayer && isAirUnit) bulletList = enemyAirBullets;

        foreach (var bullet in bulletList)
            if (!bullet.activeInHierarchy)
                return bullet;

        return null;
    }
}