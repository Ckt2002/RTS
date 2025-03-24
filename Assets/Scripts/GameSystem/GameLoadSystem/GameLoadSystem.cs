using System;
using System.Threading.Tasks;
using GameSave;
using UnityEngine;

public static class GameLoadSystem
{
    private static GameSaveData gameData;

    public static void GetGameSaveData(GameSaveData gameSaveData, Action<byte> action)
    {
        gameData = gameSaveData;

        action.Invoke(gameData.MapType);
    }

    public static async void StartLoadGame(Action action)
    {
        try
        {
            await LoadGame(action);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public static async Task LoadGame(Action action)
    {
        await LoadUnitSystem.LoadUnit(gameData.UnitDatas);

        await LoadBuildingSystem.LoadBuilding(gameData.BuildingDatas);

        await LoadBulletSystem.LoadBullet(gameData.BulletDatas);

        await LoadMatchSystem.LoadMatch(gameData.MatchData);

        await LoadResourcesSystem.LoadResources(gameData.ResourcesData);

        await LoadCreateUnitSystem.LoadCreateUnitProgress(gameData.BuyUnitDatas);

        await LoadResearchSystem.LoadResearch(gameData.ResearchDatas);

        await LoadCapturePointSystem.LoadCapturePoint(gameData.CapturePointDatas);

        await FogOfWar.Instance.LoadFogOfWar(gameData.ExploredGrid);

        action?.Invoke();
    }
}