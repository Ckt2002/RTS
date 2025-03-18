using System;
using FireBase;
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

    private bool isSaveSlot;
    public bool isCloudData { get; private set; }
    public string fileName { get; private set; }

    public void ButtonAction()
    {
        if (isSaveSlot) return;

        RunLoadGame();
    }

    public void SetSlotInfor(string createdDate, string fileName, bool isSave, bool isCloud = false)
    {
        // slotImage = img;
        slotFileCreatedDate.text = createdDate;
        slotFileName.text = fileName;
        this.fileName = fileName;
        isSaveSlot = isSave;
        isCloudData = isCloud;
        cloudIcon.gameObject.SetActive(isCloud);
    }

    private async void RunLoadGame()
    {
        try
        {
            if (cloudIcon.IsActive())
            {
                var gameData = await FirebaseLoadData.LoadFile(slotFileName.text);
                GameLoadSystem.GetGameSaveData(gameData);
            }
            else
            {
                GameLoadSystem.GetGameSaveData(FileLoadSystem.LoadGameLocal(slotFileName.text));
            }

            LoadSceneManager.Instance.StartLoadScene(nameof(Scenes.Map1), true);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading game: {e.Message}");
        }
    }
}