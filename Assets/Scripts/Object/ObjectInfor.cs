using UnityEngine;

public class ObjectInfor : MonoBehaviour
{
    [SerializeField] protected Sprite icon;
    [SerializeField] protected string description;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int armor;
    [SerializeField] protected int money;

    protected int currentHealth;

    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    public int Money => money;
    public string Description => description;
    public Sprite Icon => icon;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public void ResetStat()
    {
        currentHealth = maxHealth;
    }
}