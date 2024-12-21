using UnityEngine;

public class UnitTakeDamage : MonoBehaviour
{
    [SerializeField] private UnitInfor unitInfor;

    public void TakeDamage(int damage)
    {
        var remainDamage = damage - unitInfor.Armor;
        unitInfor.CurrentHealth -= remainDamage;
    }
}