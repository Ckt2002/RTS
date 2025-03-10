using System;

namespace GameSave
{
    [Serializable]
    public class MatchData
    {
        public int CurrentRound;
        public float TimeToNexRound;
        public SpawnEnemyData SpawnEnemyData;
    }
}