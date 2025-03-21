using System.Collections;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    private static GameResult Instance;

    [SerializeField] private GameObject InGameMenuPanel;
    [SerializeField] private GameObject InGameMenu;
    [SerializeField] private GameObject ResultMenu;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public static void GameOver()
    {
        Debug.Log("GameOver");

        Instance.StopAllCoroutines();
        ShowUI();
    }

    public static void GameComplete()
    {
        Debug.Log("You win");

        Instance.StopAllCoroutines();
        ShowUI();
    }

    private static void ShowUI()
    {
        Instance.StartCoroutine(Show());
    }

    private static IEnumerator Show()
    {
        yield return new WaitForSeconds(3f);
        Instance.InGameMenuPanel.SetActive(true);
        Instance.InGameMenu.SetActive(true);
        Instance.ResultMenu.SetActive(true);
        yield return null;
    }
}