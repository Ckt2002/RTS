using Interface;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameSlot : MonoBehaviour, IButton
{
    [SerializeField] private Text slotFileCreatedDate;
    [SerializeField] private Text slotFileName;
    [SerializeField] private Text slotFileRound;
    [SerializeField] private Image slotImage;
    [SerializeField] private Image cloudIcon;

    private bool isSaveSlot;

    // Save to cloud (firebase) and local
    public void ButtonAction()
    {
        if (isSaveSlot) return;

        // Run load game here
        RunLoadGame();
    }

    public void SetSlotInfor(string createdDate, string fileName, bool isSave)
    {
        // slotImage = img;
        slotFileCreatedDate.text = createdDate;
        slotFileName.text = fileName;
        isSaveSlot = isSave;

        // If save in firebase, show cloud icon

        // Get round by access file data
    }

    private void RunLoadGame()
    {
        GameLoadSystem.GetGameSaveData(FileLoadSystem.LoadGameLocal(slotFileName.text));

        // Start Load Scene
        LoadSceneManager.Instance.StartLoadScene(nameof(Scenes.Map1), true);
    }
}