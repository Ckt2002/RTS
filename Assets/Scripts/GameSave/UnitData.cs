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
        public PositionData TargetPosition;
        public PositionData Velocity;
        public bool IsMoving;
        public ExplodeParticleData ParticleData;
    }
}