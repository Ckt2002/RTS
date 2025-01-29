using UnityEngine;

public class ObjectTakeDamage : MonoBehaviour
{
    [SerializeField] private ObjectInfor objectInfor;

    public void TakeDamage(int damage)
    {
        var remainDamage = damage - objectInfor.Armor;
        if (remainDamage < 0)
            return;
        objectInfor.CurrentHealth -= remainDamage;
    }
}