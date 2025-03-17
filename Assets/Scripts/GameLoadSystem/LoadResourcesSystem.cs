using System.Collections;
using GameSave;
using UnityEngine;

public class LoadResourcesSystem
{
    public static IEnumerator LoadResources(ResourcesData resourcesData)
    {
        ResourcesManager.Instance.Money = resourcesData.Money;
        yield return null;
    }
}