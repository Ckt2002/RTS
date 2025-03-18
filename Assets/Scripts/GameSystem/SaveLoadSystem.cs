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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
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
            var fileName = Path.GetFileName(filePath);
            var creationTime = File.GetCreationTime(filePath);
            var formattedCreationTime = creationTime.ToString("HH:mm-dd/MM/yyyy");

            var slot = new LoadSlotInfor();
            slot.SetInfor(fileName, formattedCreationTime, false);
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
            slot.SetInfor(data.Key, data.Value.saveTime, true);
            AddNewSlot(slot);
        }
    }
}