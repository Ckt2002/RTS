using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
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

        public void StartLoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            StartCoroutine(RunLoadingSceneProgress(sceneName));
        }

        private IEnumerator RunLoadingSceneProgress(string sceneName)
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
                    yield return LoadingMap();
                    break;

                case nameof(Scenes.MainMenu):
                    yield return LoadingMenu();
                    break;
            }

            yield return new WaitForSeconds(1f);
            LoadScene.Instance?.HideLoadScene();
            MatchController.Instance?.StartMatch();
        }

        private void RunPoolingSystem(Action action)
        {
            StringBuilder a = new();
            action?.Invoke();
        }

        private IEnumerator LoadingMap()
        {
            var poolingCompleted = 0;
            var loadProgress = 0f;
            var totalPoolingSystem = 3;
            yield return new WaitForSeconds(1f);

            RunPoolingSystem(() => BuildingPooling.Instance.RunSpawnObjects(() => poolingCompleted++));
            RunPoolingSystem(() => UnitPooling.Instance.RunSpawnObjects(() => poolingCompleted++));
            RunPoolingSystem(() => BulletPooling.Instance.RunSpawnObjects(() => poolingCompleted++));

            while (loadProgress <= 1f)
            {
                var progressValue = loadProgress / 1;
                LoadScene.Instance.SetSliderValue(progressValue);
                loadProgress += Time.deltaTime * 2;
                if ((loadProgress >= 0.3f && loadProgress <= 0.32f) || poolingCompleted < totalPoolingSystem)
                    yield return new WaitForSeconds(3f);
                else
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
                // Debug.Log(loadProgress);
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
}