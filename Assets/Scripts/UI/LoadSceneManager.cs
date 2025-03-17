using System;
using System.Collections;
using GameSystem;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance;

    private LoadScene loading;

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

    public void StartLoadScene(string sceneName, bool isLoadGameSave)
    {
        SceneManager.LoadScene(sceneName);
        StartCoroutine(RunLoadingSceneProgress(sceneName, isLoadGameSave));
    }

    private IEnumerator RunLoadingSceneProgress(string sceneName, bool isLoadGameSave)
    {
        LoadScene loadScene = null;

        while (loadScene == null)
        {
            loadScene = LoadScene.Instance;
            yield return null;
        }

        loadScene.ShowLoadScene();

        switch (sceneName)
        {
            case nameof(Scenes.Map1):
            case nameof(Scenes.Map2):
            case nameof(Scenes.Map3):
                yield return LoadingMap(isLoadGameSave);
                break;

            case nameof(Scenes.MainMenu):
                yield return LoadingMenu();
                break;
        }

        yield return new WaitForSeconds(1f);
        LoadScene.Instance?.HideLoadScene();
        MatchController.Instance?.StartMatch();

        PauseSystem.ResumeGame();
    }

    private void RunPoolingSystem(Action action)
    {
        action?.Invoke();
    }

    private void RunLoadGameSystem(Action action)
    {
        action?.Invoke();
    }

    private IEnumerator LoadingMap(bool isLoadGameSave)
    {
        var poolingCompleted = 0;
        var loadProgress = 0f;
        var totalPoolingSystem = 3;
        var loadSaveGameCompleted = false;
        yield return new WaitForSeconds(1f);
        PauseSystem.PauseGame();

        RunPoolingSystem(() => BuildingPooling.Instance.RunSpawnObjects(() => poolingCompleted++));
        RunPoolingSystem(() => UnitPooling.Instance.RunSpawnObjects(() => poolingCompleted++));
        RunPoolingSystem(() => BulletPooling.Instance.RunSpawnObjects(() => poolingCompleted++));

        while (poolingCompleted < totalPoolingSystem)
        {
            if (loadProgress <= 0.31f)
            {
                loadProgress += Time.deltaTime;
                LoadScene.Instance.SetSliderValue(loadProgress);
            }

            yield return null;
        }


        if (isLoadGameSave)
        {
            // RunLoad game here
            Debug.Log("Loading game");
            RunLoadGameSystem(() => GameLoadSystem.StartLoadGame(() => loadSaveGameCompleted = true));

            // Đợi cho đến khi LoadGame hoàn thành
            while (!loadSaveGameCompleted)
            {
                if (loadProgress <= 0.8f)
                {
                    loadProgress += Time.deltaTime;
                    LoadScene.Instance.SetSliderValue(loadProgress);
                }

                yield return null;
            }
        }

        while (loadProgress < 1f)
        {
            loadProgress += Time.deltaTime * 2;
            LoadScene.Instance.SetSliderValue(loadProgress);
            yield return null;
        }

        LoadScene.Instance.SetSliderValue(1f);
        yield return null;
    }

    private IEnumerator LoadingMenu()
    {
        var loadProgress = 0f;
        yield return new WaitForSeconds(1f);

        while (loadProgress <= 1f)
        {
            var progressValue = loadProgress / 1;
            LoadScene.Instance.SetSliderValue(progressValue);
            loadProgress += Time.deltaTime * 2;

            if (loadProgress >= 0.3f && loadProgress <= 0.31f)
                yield return new WaitForSeconds(1.5f);
            else
                yield return null;
        }

        LoadScene.Instance.SetSliderValue(1f);
        yield return null;
    }
}