using GameSystem;
using TMPro;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager Instance;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private int startMoney;

    public int Money { get; set; }

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
        Money = startMoney;
    }

    private void Update()
    {
        if (PauseSystem.isPausing) return;

        moneyText.text = $"{Money}$";
    }

    public void CurrentMoney(int amount)
    {
        Money += amount;
    }
}