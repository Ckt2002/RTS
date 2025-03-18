using System;
using System.Collections.Generic;
using System.IO;
using GameSave;
using UnityEngine;

public class SaveLoadGameUI : MonoBehaviour
{
    [SerializeField] private GameObject loadGameSlotButton;
    [SerializeField] private Transform content;
    [SerializeField] private bool isSave;

    private Dictionary<string, CloudData> cloudSaveData;
    private Dictionary<string, GameSaveData> localSaveData;

    private void Start()
    {
        GetLocalSaveFiles();
        GetCloudSaveFiles();
    }

    private void GetLocalSaveFiles()
    {
        var saveFiles = SaveLoadSystem.Instance?.GetAllSaveFile();
        if (saveFiles == null) return;
        if (saveFiles.Length == 0) return;

        foreach (var filePath in saveFiles)
        {
            var fileName = Path.GetFileName(filePath);
            var creationTime = File.GetCreationTime(filePath);
            var formattedCreationTime = creationTime.ToString("HH:mm-dd/MM/yyyy");

            var slot = Instantiate(loadGameSlotButton, content);
            slot.GetComponent<LoadGameSlot>().SetSlotInfor(formattedCreationTime, fileName, isSave);
        }
    }

    private void GetFileName(Dictionary<string, CloudData> cloudData)
    {
        if (cloudData == null || cloudData.Count == 0)
            return;

        foreach (var data in cloudData)
        {
            var slot = Instantiate(loadGameSlotButton, content);
            slot.GetComponent<LoadGameSlot>().SetSlotInfor(data.Value.saveTime, data.Key, isSave, true);
        }

        cloudSaveData = cloudData;
    }

    private async void GetCloudSaveFiles()
    {
        try
        {
            await FirebaseGetData.GetFileNames(GetFileName);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error getting file save: {e.Message}");
        }
    }

    public void ShowSlot(GameObject go)
    {
        go.SetActive(true);
    }

    public void HideSlot(GameObject go)
    {
        go.SetActive(false);
    }
}