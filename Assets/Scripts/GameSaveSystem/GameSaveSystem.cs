using GameSave;

public class GameSaveSystem
{
    public static GameSaveData GameData()
    {
        var saveData = new GameSaveData
        {
            UnitDatas = SaveUnitSystem.SaveUnits(),
            BuildingDatas = SaveBuildingSystem.SaveBuildings(),
            BulletDatas = SaveBulletSystem.SaveBullets(),
            ResearchDatas = SaveResearchSystem.SaveResearchDatas(),
            ResourcesData = SaveResourcesSystem.SaveResourcesData(),
            MatchData = SaveMatchSystem.matchData,
            BuyUnitDatas = SaveCreateUnitSystem.SaveCreateUnit(),
            CapturePointDatas = SaveCapturePointSystem.SaveCapturePoint()
        };

        // var json = JsonUtility.ToJson(saveData, true);
        // Debug.Log(json);
        return saveData;
    }
}