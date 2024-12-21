using UnityEngine;

public class UnitInfor : ObjectInfor
{
    [SerializeField] private int damage;
    [SerializeField] private int speed;
    [SerializeField] private float buyTime;
    [SerializeField] private float fireRate;
    [SerializeField] private float researchTime;

    public int Damage => damage;
    public int Speed => speed;
    public float FireRate => fireRate;
    public float BuyTime => buyTime;
    public int Armor => armor;
    public float ResearchTime => researchTime;
}