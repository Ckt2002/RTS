using System;
using System.IO;
using Assets.Scripts.Data;
using GameSave;
using UnityEngine;

public static class FileSaveSystem
{
    public static bool isCloud { private get; set; }

    public static void SaveGame(string fileName)
    {
        if (!isCloud)
            SaveGameLocal(GameSaveSystem.GameData(), fileName);
        else
            SaveGameCloud(GameSaveSystem.GameData(), fileName);
    }

    private static void SaveGameLocal(GameSaveData gameData, string fileName)
    {
        try
        {
            var json = JsonUtility.ToJson(gameData, true);
            // var filePath = Application.persistentDataPath + "/" + fileName + ".json";

            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, fileName + ".json");

            File.WriteAllText(filePath, json);
            var saveTime = DateTime.Now.ToString("HH:mm-dd/MM/yyyy");
            UpdateSaveLoadSystem(fileName, saveTime, false);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving data: " + ex.Message);
        }
    }

    private static async void SaveGameCloud(GameSaveData gameData, string fileName)
    {
        try
        {
            var json = JsonUtility.ToJson(gameData, true);
            var saveTime = DateTime.Now.ToString("HH:mm-dd/MM/yyyy");
            await FirebaseSaveData.SaveData(fileName, json, saveTime);
            UpdateSaveLoadSystem(fileName, saveTime, true);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving data: " + ex.Message);
        }
    }

    private static void UpdateSaveLoadSystem(string fileName, string saveTime, bool isCloud)
    {
        var slotInfo = new LoadSlotInfor();
        slotInfo.SetInfor(fileName, saveTime, isCloud);
        SaveLoadSystem.Instance.AddNewSlot(slotInfo);
    }
}