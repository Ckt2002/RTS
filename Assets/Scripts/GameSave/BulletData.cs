using System;

namespace GameSave
{
    [Serializable]
    public class BulletData
    {
        public int CurrentDamage;
        public ObjectData Obj;
        public PositionData Position;
        public RotationData Rotation;
    }
}