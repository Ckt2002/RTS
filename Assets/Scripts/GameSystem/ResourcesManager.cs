using TMPro;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager Instance;
    [SerializeField] private TMP_Text moneyText;

    public int Money { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        transform.SetParent(null);
    }

    private void Start()
    {
        Money = 5000;
    }

    private void Update()
    {
        moneyText.text = $"{Money}$";
    }

    public void CurrentMoney(int amount)
    {
        Money += amount;
    }
}