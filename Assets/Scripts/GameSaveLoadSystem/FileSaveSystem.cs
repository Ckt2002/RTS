using System.IO;
using GameSave;
using UnityEngine;

public class FileSaveSystem : MonoBehaviour
{
    public static FileSaveSystem Instance;

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

    public void SaveGameLocal(GameSaveData gameData, string fileName)
    {
        var json = JsonUtility.ToJson(gameData);
        var filePath = Application.persistentDataPath + "/" + fileName + ".json";
        File.WriteAllText(filePath, json);

        Debug.Log("Data saved to: " + filePath);
    }

    public GameSaveData LoadGameLocal(string fileName)
    {
        var filePath = Application.persistentDataPath + "/" + fileName + ".json";

        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);

            var data = JsonUtility.FromJson<GameSaveData>(json);
            return data;
        }

        Debug.LogWarning("File not found: " + filePath);
        return null;
    }
}