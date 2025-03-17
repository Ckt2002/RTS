using System;
using System.Collections;
using GameSave;
using UnityEngine;

public class GameLoadSystem
{
    private static GameSaveData gameData;

    public static void GetGameSaveData(GameSaveData gameSaveData)
    {
        gameData = gameSaveData;
    }

    public static void StartLoadGame(Action action)
    {
        CoroutineManager.Instance.StartManagedCoroutine(LoadGame(action));
    }

    public static IEnumerator LoadGame(Action action)
    {
        Debug.Log("Unit Loading");

        yield return LoadUnitSystem.LoadUnit(gameData.UnitDatas);

        Debug.Log("Unit Loaded");

        yield return LoadBuildingSystem.LoadBuilding(gameData.BuildingDatas);

        yield return LoadBulletSystem.LoadBullet(gameData.BulletDatas);

        yield return LoadMatchSystem.LoadMatch(gameData.MatchData);

        yield return LoadResourcesSystem.LoadResources(gameData.ResourcesData);

        yield return LoadCreateUnitSystem.LoadCreateUnitProgress(gameData.BuyUnitDatas);

        yield return LoadResearchSystem.LoadResearch(gameData.ResearchDatas);

        yield return LoadCapturePointSystem.LoadCapturePoint(gameData.CapturePointDatas);

        action?.Invoke();
    }
}