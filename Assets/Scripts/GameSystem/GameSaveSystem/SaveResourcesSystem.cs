using GameSave;
using UnityEngine;

public class SaveResourcesSystem
{
    public static ResourcesData SaveResourcesData()
    {
        return new ResourcesData { Money = ResourcesManager.Instance.Money };
    }
}