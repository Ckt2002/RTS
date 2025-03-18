using Interface;
using UnityEngine;
using UnityEngine.UI;

public class CloudButton : MonoBehaviour, IButton
{
    [SerializeField] private Image buttonImage;

    private bool selected;

    private void Start()
    {
        FileSaveSystem.isCloud = selected;
    }

    public void ButtonAction()
    {
        selected = !selected;

        if (selected)
        {
            FileSaveSystem.isCloud = true;
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.b, buttonImage.color.g, 255);
            return;
        }

        FileSaveSystem.isCloud = false;
        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.b, buttonImage.color.g, 207);
    }
}