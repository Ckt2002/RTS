using System;
using FireBase;
using Interface;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameSlot : MonoBehaviour, IButton
{
    [SerializeField] private Text slotFileCreatedDate;
    [SerializeField] private Text slotFileName;
    [SerializeField] private Image slotImage;
    [SerializeField] private Image cloudIcon;
    [SerializeField] private Sprite[] slotImages;

    private bool isSaveSlot;
    public bool isCloudData { get; private set; }
    public string fileName { get; private set; }
    public int dataIndex { get; private set; }

    public void ButtonAction()
    {
        if (isSaveSlot) return;

        RunLoadGame();
    }

    public void SetSlotInfor(int dataIndex, string createdDate, string fileName, bool isSave,
        bool isCloud = false, int mapType = 1)
    {
        slotImage.sprite = slotImages[mapType - 1];
        this.dataIndex = dataIndex;
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
            var sceneInd = 1;
            if (cloudIcon.IsActive())
            {
                var gameData = await FirebaseLoadData.LoadFile(slotFileName.text);
                GameLoadSystem.GetGameSaveData(gameData, value => sceneInd = value);
            }
            else
            {
                GameLoadSystem.GetGameSaveData(FileLoadSystem.LoadGameLocal(slotFileName.text),
                    value => sceneInd = value);
            }

            var sceneType = (Scenes)sceneInd;
            LoadSceneManager.Instance.StartLoadScene(sceneType.ToString(), true);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading game: {e.Message}");
        }
    }
}