using System.IO;
using UnityEngine;

public class SaveLoadGameUI : MonoBehaviour
{
    [SerializeField] private GameObject loadGameSlotButton;
    [SerializeField] private Transform content;
    [SerializeField] private bool isSave;

    private void Start()
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

    public void ShowSlot(GameObject go)
    {
        go.SetActive(true);
    }

    public void HideSlot(GameObject go)
    {
        go.SetActive(false);
    }
}