using Interface;
using UnityEngine;

public class ExitButton : MonoBehaviour, IButton
{
    public void ButtonAction()
    {
        Application.Quit();
    }
}