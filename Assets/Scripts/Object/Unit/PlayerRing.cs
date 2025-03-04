using UnityEngine;

public class PlayerRing : MonoBehaviour
{
    [SerializeField] private GameObject unitRing;

    public bool isSelected { get; private set; }

    public void ShowRing()
    {
        unitRing.SetActive(isSelected = true);
    }

    public void HideRing()
    {
        unitRing.SetActive(isSelected = false);
    }
}