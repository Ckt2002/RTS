using System.Collections;
using GameSave;
using UnityEngine;

public class LoadResourcesSystem : MonoBehaviour
{
    public static IEnumerator LoadResources(ResourcesData resourcesData)
    {
        ResourcesManager.Instance.Money = resourcesData.Money;
        yield return null;
    }
}