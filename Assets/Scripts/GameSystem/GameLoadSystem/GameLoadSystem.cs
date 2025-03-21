using System;
using System.Threading.Tasks;
using GameSave;

public class GameLoadSystem
{
    private static GameSaveData gameData;

    public static void GetGameSaveData(GameSaveData gameSaveData)
    {
        gameData = gameSaveData;
    }

    public static async void StartLoadGame(Action action)
    {
        await LoadGame(action);
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

        action?.Invoke();
    }
}