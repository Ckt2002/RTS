using System;

namespace GameSave
{
    [Serializable]
    public class MatchData
    {
        public int CurrentRound;
        public float RoundTimer;
        public bool IsSpawning;
        public bool WaitingForNextRound;
        public SpawnEnemyData SpawnEnemyData;
    }
}