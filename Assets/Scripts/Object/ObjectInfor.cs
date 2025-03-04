using UnityEngine;

public class ObjectInfor : MonoBehaviour
{
    [SerializeField] protected Sprite icon;
    [SerializeField] protected string description;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int armor;
    [SerializeField] protected int money;
    [SerializeField] protected float vision;

    public int CurrentHealth { get; set; } = 1;

    public int Money => money;
    public string Description => description;
    public Sprite Icon => icon;
    public int Armor => armor;
    public float Vision => vision;

    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public bool IsAlive()
    {
        return CurrentHealth > 0;
    }

    public void ResetStat()
    {
        CurrentHealth = maxHealth;
    }
}