using System;
using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string[] GetAllSaveFile()
    {
        try
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var saveFiles = Directory.GetFiles(desktopPath, "*.json");

            if (saveFiles.Length == 0)
            {
                Debug.LogWarning("No save files found.");
                return null;
            }

            return saveFiles;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error retrieving save files: " + ex.Message);
            return null;
        }
    }
}