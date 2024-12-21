using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDescription : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text description;

    public void SetDescription(Sprite icon, int money, string description)
    {
        this.icon.sprite = icon;
        moneyText.text = $"{money.ToString()}$";
        this.description.text = description;
    }
}
