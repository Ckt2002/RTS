using System;
using System.IO;
using UnityEngine;

public class DeleteFileSaveSystem
{
    public static void DeleteFileLocal(string fileName)
    {
        try
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, fileName + ".json");

            if (File.Exists(filePath))
                File.Delete(filePath);
            // Debug.Log($"File deleted successfully: {filePath}");
            else
                Debug.LogWarning($"File not found: {filePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error deleting file: " + ex.Message);
        }
    }
}