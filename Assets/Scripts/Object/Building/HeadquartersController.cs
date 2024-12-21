using UnityEngine;

public class HeadquartersController : BuildingController
{
    [SerializeField] private int money;
    [SerializeField] private float timeToGetMoney;

    private ResourcesManager resourcesManager;

    private void Start()
    {
        resourcesManager = ResourcesManager.Instance;
        InvokeRepeating(nameof(CreateMoney), timeToGetMoney, timeToGetMoney);
    }

    private void CreateMoney()
    {
        resourcesManager.CurrentMoney(money);
    }
}