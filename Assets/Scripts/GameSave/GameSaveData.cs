using System.Collections.Generic;

namespace GameSave
{
    public class GameSaveData
    {
        // unit list
        public List<UnitData> UnitDatas;

        // building list
        public List<BuildingData> BuildingDatas;

        // building list
        public List<BulletData> BulletDatas;

        // progress list
        public MatchData MatchData;
        public List<SpawnEnemyData> SpawnEnemyDatas;
        public List<CapturePointData> CapturePointDatas;
    }
}