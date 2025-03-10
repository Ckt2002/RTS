using System.Collections.Generic;

namespace GameSave
{
    public class GameSaveData
    {
        public List<UnitData> UnitDatas;

        public List<BuildingData> BuildingDatas;

        public List<BulletData> BulletDatas;

        public MatchData MatchData;
        public BuyUnitData BuyUnitData;
        public List<ResearchData> ResearchDatas;
        public List<CapturePointData> CapturePointDatas;
    }
}