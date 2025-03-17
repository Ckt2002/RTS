using System;
using System.IO;
using GameSave;
using UnityEngine;

public class FileLoadSystem
{
    public static GameSaveData LoadGameLocal(string fileName)
    {
        try
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, fileName);

            if (!File.Exists(filePath))
            {
                Debug.LogError("File not found: " + filePath);
                return null;
            }

            var json = File.ReadAllText(filePath);

            var gameData = JsonUtility.FromJson<GameSaveData>(json);
            return gameData;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error loading data: " + ex.Message);
            return null;
        }
    }
}