using GameSave;
using UnityEngine;

public class SaveMatchSystem : MonoBehaviour
{
    public static MatchData matchData;

    public static MatchData ReturnMatchData()
    {
        return matchData;
    }

    public static void GetMatchData(int currentRound, float timeToNextRound, SpawnEnemyData spawnEnemyData)
    {
        matchData = new MatchData
        {
            CurrentRound = currentRound,
            TimeToNexRound = timeToNextRound,
            SpawnEnemyData = spawnEnemyData
        };
    }
}