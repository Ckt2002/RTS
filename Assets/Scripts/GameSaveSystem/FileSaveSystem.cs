using System;
using System.IO;
using GameSave;
using UnityEngine;

public class FileSaveSystem : MonoBehaviour
{
    public static void SaveGameLocal(GameSaveData gameData, string fileName)
    {
        try
        {
            var json = JsonUtility.ToJson(gameData);
            // var filePath = Application.persistentDataPath + "/" + fileName + ".json";

            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, fileName + ".json");

            File.WriteAllText(filePath, json);
            Debug.Log("Data saved to: " + filePath);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error encrypting data: " + ex.Message);
        }
    }
}