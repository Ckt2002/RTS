using Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour, IButton
{
    [SerializeField] private SceneName sceneName;

    public void ButtonAction()
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}