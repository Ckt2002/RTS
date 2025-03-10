using System;
using UnityEngine;

namespace GameSave
{
    [Serializable]
    public class PositionData
    {
        public float X;
        public float Y;
        public float Z;

        public void GetPosition(Vector3 pos)
        {
            X = pos.x;
            Y = pos.y;
            Z = pos.z;
        }
    }
}