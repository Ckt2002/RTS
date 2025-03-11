using System.Collections;
using GameSave;
using UnityEngine;

public class LoadMatchSystem : MonoBehaviour
{
    public static IEnumerator LoadMatch(MatchData matchData)
    {
        Debug.Log(matchData.CurrentRound);
        yield return LoadCreateEnemySystem.LoadCreateEnemy(matchData.SpawnEnemyData);
        yield return null;
    }
}