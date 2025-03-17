using System.Collections;
using GameSave;
using UnityEngine;

public class LoadMatchSystem
{
    public static IEnumerator LoadMatch(MatchData matchData)
    {
        MatchController.Instance.LoadMatch(matchData.CurrentRound, matchData.TimeToNexRound);
        yield return LoadCreateEnemySystem.LoadCreateEnemy(matchData.SpawnEnemyData);
        yield return null;
    }
}