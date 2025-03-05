using Interface;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameSlot : MonoBehaviour, IButton
{
    [SerializeField] private Text slotFileCreatedDate;
    [SerializeField] private Text slotFileName;
    [SerializeField] private Text slotFileRound;
    [SerializeField] private Image slotImage;
    [SerializeField] private Image cloudIcon;

    // Save to cloud (firebase) and local
    void IButton.ButtonAction()
    {
    }

    public void SetSlotInfor(Image img, string createdDate, string fileName)
    {
        slotImage = img;
        slotFileCreatedDate.text = createdDate;
        slotFileName.text = fileName;

        // If save in firebase, show cloud icon

        // Get round by access file data
    }
}