using System;

namespace GameSave
{
    [Serializable]
    public class BuildingData
    {
        public ObjectData Obj;
        public StatData Stat;
        public PositionData Position;
        public RotationData Rotation;
        public ObjectDieData DieData;
    }
}