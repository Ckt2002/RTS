using System.Collections.Generic;

namespace GameSave
{
    public class GameSaveData
    {
        public byte MapType = 1;

        public List<UnitData> UnitDatas;

        public List<BuildingData> BuildingDatas;

        public List<BulletData> BulletDatas;

        public ResourcesData ResourcesData;
        public MatchData MatchData;
        public List<BuyUnitData> BuyUnitDatas;
        public List<ResearchData> ResearchDatas;
        public List<CapturePointData> CapturePointDatas;
        public bool[] ExploredGrid;
    }
}