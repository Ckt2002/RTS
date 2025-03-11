using System;
using UnityEngine;

namespace GameSave
{
    [Serializable]
    public class RotationData
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public void GetRotation(Quaternion rot)
        {
            X = rot.x;
            Y = rot.y;
            Z = rot.z;
            W = rot.w;
        }

        public Quaternion SetRotation()
        {
            return new Quaternion(X, Y, Z, W);
        }
    }
}