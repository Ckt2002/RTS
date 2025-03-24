using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Data;
using GameSave;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem Instance;

    public List<LoadSlotInfor> LoadGameSlots { get; private set; }
    public event Action OnSlotsUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadGameSlots = new List<LoadSlotInfor>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        GetLocalSaveFiles();
        GetCloudSaveFiles();
    }

    public void AddNewSlot(LoadSlotInfor slot)
    {
        if (!LoadGameSlots.Contains(slot))
        {
            LoadGameSlots.Add(slot);
            OnSlotsUpdated?.Invoke();
        }
    }

    private void GetLocalSaveFiles()
    {
        var saveFiles = GetLocalFiles.GetAllSaveFile();
        if (saveFiles == null || saveFiles.Length == 0) return;

        foreach (var filePath in saveFiles)
        {
            var fileContent = File.ReadAllText(filePath);
            var saveData = JsonUtility.FromJson<GameSaveData>(fileContent);

            var mapType = saveData.MapType;
            var fileName = Path.GetFileName(filePath).Replace(".json", "");
            var creationTime = File.GetCreationTime(filePath);
            var formattedCreationTime = creationTime.ToString("HH:mm-dd/MM/yyyy");

            var slot = new LoadSlotInfor();
            slot.SetInfor(fileName, formattedCreationTime, false, mapType);
            AddNewSlot(slot);
        }
    }

    private async void GetCloudSaveFiles()
    {
        try
        {
            await FirebaseGetData.GetFileNames(GetCloudFileName);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error getting file save: {e.Message}");
        }
    }

    private void GetCloudFileName(Dictionary<string, CloudData> cloudData)
    {
        if (cloudData == null || cloudData.Count == 0) return;

        foreach (var data in cloudData)
        {
            var slot = new LoadSlotInfor();
            var mapType = data.Value.gameData.MapType;
            slot.SetInfor(data.Key, data.Value.saveTime, true, mapType);
            AddNewSlot(slot);
        }
    }
}