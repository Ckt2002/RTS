using System;
using System.IO;
using GameSave;
using UnityEngine;

public class FileSaveSystem : MonoBehaviour
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
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving data: " + ex.Message);
        }
    }

    private static void SaveGameCloud(GameSaveData gameData, string fileName)
    {
        try
        {
            var json = JsonUtility.ToJson(gameData, true);
            CoroutineManager.Instance.StartCoroutine(FirebaseSaveData.SaveData(fileName, json));
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving data: " + ex.Message);
        }
    }
}