using Interface;
using UI;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour, IButton
{
    [SerializeField] private Scenes scenes;

    public void ButtonAction()
    {
        LoadSceneManager.Instance.StartLoadScene(scenes.ToString());
    }
}