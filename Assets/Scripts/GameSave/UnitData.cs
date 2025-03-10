using System;

namespace GameSave
{
    [Serializable]
    public class UnitData
    {
        public ObjectData Obj;
        public StatData Stat;
        public PositionData Position;
        public RotationData Rotation;
        public ExplodeParticleData ParticleData;
    }
}