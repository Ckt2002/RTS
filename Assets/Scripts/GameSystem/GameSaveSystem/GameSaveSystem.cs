using GameSave;
using UnityEngine.SceneManagement;

public static class GameSaveSystem
{
    public static GameSaveData GameData()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        var saveData = new GameSaveData
        {
            MapType = (byte)currentSceneIndex,
            UnitDatas = SaveUnitSystem.SaveUnits(),
            BuildingDatas = SaveBuildingSystem.SaveBuildings(),
            BulletDatas = SaveBulletSystem.SaveBullets(),
            ResearchDatas = SaveResearchSystem.SaveResearchDatas(),
            ResourcesData = SaveResourcesSystem.SaveResourcesData(),
            MatchData = SaveMatchSystem.SaveMatchData(),
            BuyUnitDatas = SaveCreateUnitSystem.SaveCreateUnit(),
            CapturePointDatas = SaveCapturePointSystem.SaveCapturePoint(),
            ExploredGrid = FogOfWar.Instance.exploredGrid
        };

        return saveData;
    }
}