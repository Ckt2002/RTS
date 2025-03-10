using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class SaveBulletSystem : MonoBehaviour
{
    public static List<BulletData> SaveBullets()
    {
        var bulletsActivedOnMap = BulletPooling.Instance.GetObjectsToSave();
        var bulletDatas = new List<BulletData>();
        foreach (var bulletType in bulletsActivedOnMap)
        foreach (var bullet in bulletType.Value)
        {
            var data = new BulletData
            {
                Obj = new ObjectData(),
                Position = new PositionData(),
                Rotation = new RotationData()
            };
            data.CurrentDamage = bullet.Value.GetComponent<UnitBullet>().Damage;
            data.Obj.Name = bulletType.Key;
            data.Obj.LstIndex = bullet.Key;
            data.Position.GetPosition(bullet.Value.transform.position);
            data.Rotation.GetRotation(bullet.Value.transform.localRotation);

            bulletDatas.Add(data);
        }

        return bulletDatas;
    }
}