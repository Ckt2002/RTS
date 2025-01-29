using UnityEngine;

public class PlayerRing : MonoBehaviour
{
    [SerializeField] private GameObject unitRing;

    public bool isSelected { get; private set; }

    public void UnitSelected()
    {
        unitRing.SetActive(isSelected = true);
    }

    public void UnitDeselected()
    {
        unitRing.SetActive(isSelected = false);
    }
}