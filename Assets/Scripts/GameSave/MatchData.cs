using System;
using UnityEngine.Serialization;

namespace GameSave
{
    [Serializable]
    public class MatchData
    {
        public int CurrentRound;

        [FormerlySerializedAs("Timer")] [FormerlySerializedAs("TimeToNexRound")]
        public float RoundTimer;

        [FormerlySerializedAs("IsInMatch")] public bool IsSpawning;
        public SpawnEnemyData SpawnEnemyData;
    }
}