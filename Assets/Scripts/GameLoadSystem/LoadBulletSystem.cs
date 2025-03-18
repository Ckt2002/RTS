using System.Collections.Generic;
using System.Threading.Tasks;
using GameSave;

public class LoadBulletSystem
{
    public static Task LoadBullet(List<BulletData> bulletDatas)
    {
        if (bulletDatas == null) return Task.CompletedTask;

        var bulletDictionary = BulletPooling.Instance.GetObjectDictionary;
        foreach (var data in bulletDatas)
        {
            var bullet = bulletDictionary[data.Obj.Name][data.Obj.LstIndex];
            bullet.transform.position = data.Position.SetPosition();
            bullet.transform.localRotation = data.Rotation.SetRotation();
            bullet.GetComponent<UnitBullet>().SetupDamage(data.CurrentDamage);
            bullet.SetActive(true);
        }

        return Task.CompletedTask;
    }
}