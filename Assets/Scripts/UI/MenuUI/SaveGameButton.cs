using Interface;
using TMPro;
using UnityEngine;

public class SaveGameButton : MonoBehaviour, IButton
{
    [SerializeField] private TMP_InputField inputName;

    public void ButtonAction()
    {
        if (inputName.text.Equals("")) return;
        FileSaveSystem.SaveGameLocal(
            LoadGameSystem.SaveGame(), inputName.text);
    }
}