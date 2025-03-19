using System;

namespace GameSave
{
    [Serializable]
    public class MatchData
    {
        public int CurrentRound;
        public float TimeToNexRound;
        public bool IsInMatch;
        public SpawnEnemyData SpawnEnemyData;
    }
}