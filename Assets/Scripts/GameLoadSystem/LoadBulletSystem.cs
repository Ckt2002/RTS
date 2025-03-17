using System.Collections;
using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class LoadBulletSystem
{
    public static IEnumerator LoadBullet(List<BulletData> bulletDatas)
    {
        var bulletDictionary = BulletPooling.Instance.GetObjectDictionary;
        foreach (var data in bulletDatas)
        {
            var bullet = bulletDictionary[data.Obj.Name][data.Obj.LstIndex];
            bullet.transform.position = data.Position.SetPosition();
            bullet.transform.localRotation = data.Rotation.SetRotation();
            bullet.GetComponent<UnitBullet>().SetupDamage(data.CurrentDamage);
            bullet.SetActive(true);
        }

        yield return null;
    }
}