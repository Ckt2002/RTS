using System.Threading.Tasks;
using GameSave;

public class LoadResourcesSystem
{
    public static Task LoadResources(ResourcesData resourcesData)
    {
        if (resourcesData == null) return Task.CompletedTask;

        ResourcesManager.Instance.Money = resourcesData.Money;
        return Task.CompletedTask;
    }
}