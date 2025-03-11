using System.Collections;
using System.Collections.Generic;
using GameSave;
using UnityEngine;

public class LoadBulletSystem : MonoBehaviour
{
    public static IEnumerable LoadBullet(List<BulletData> bulletDatas)
    {
        foreach (var data in bulletDatas) Debug.Log(data.Obj.Name);
        yield return null;
    }
}