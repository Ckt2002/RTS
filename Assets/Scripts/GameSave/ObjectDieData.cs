using System;

namespace GameSave
{
    [Serializable]
    public class ObjectDieData
    {
        public bool RunExplodeAndSound;
        public float ParticleElapsedTime;
        public float ResetElapsedTime;
    }
}