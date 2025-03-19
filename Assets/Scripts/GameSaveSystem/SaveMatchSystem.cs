using GameSave;

public static class SaveMatchSystem
{
    public static MatchData matchData;

    public static MatchData ReturnMatchData()
    {
        return matchData;
    }

    public static void GetMatchData(int currentRound, float timeToNextRound, bool isInMatch,
        SpawnEnemyData spawnEnemyData)
    {
        matchData = new MatchData
        {
            CurrentRound = currentRound,
            TimeToNexRound = timeToNextRound,
            IsInMatch = isInMatch,
            SpawnEnemyData = spawnEnemyData
        };
    }
}