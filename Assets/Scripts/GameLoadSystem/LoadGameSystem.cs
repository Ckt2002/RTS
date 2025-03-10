using GameSave;
using UnityEngine;

public class LoadGameSystem : MonoBehaviour
{
    public static GameSaveData SaveGame()
    {
        var saveData = new GameSaveData
        {
            UnitDatas = SaveUnitSystem.SaveUnits(),
            BuildingDatas = SaveBuildingSystem.SaveBuildings(),
            BulletDatas = SaveBulletSystem.SaveBullets(),
            ResearchDatas = SaveResearchSystem.SaveResearchDatas(),
            MatchData = SaveMatchSystem.matchData,
            CapturePointDatas = SaveCapturePointSystem.SaveCapturePoint()
        };

        var json = JsonUtility.ToJson(saveData, true);
        Debug.Log(json);
        return saveData;
    }
}