using UnityEngine;

public class ObjectTakeDamage : MonoBehaviour
{
    [SerializeField] private ObjectInfor objectInfor;

    public void TakeDamage(int damage)
    {
        var remainDamage = damage - objectInfor.Armor;

        remainDamage = Mathf.Max(1, remainDamage);

        objectInfor.CurrentHealth -= remainDamage;
    }
}