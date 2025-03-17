using GameSave;
using UnityEngine;

public class SaveResourcesSystem : MonoBehaviour
{
    public static ResourcesData SaveResourcesData()
    {
        return new ResourcesData { Money = ResourcesManager.Instance.Money };
    }
}