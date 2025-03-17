using System.Collections;
using GameSave;
using UnityEngine;

public class LoadCreateEnemySystem : MonoBehaviour
{
    public static IEnumerator LoadCreateEnemy(SpawnEnemyData spawnEnemyData)
    {
        if (spawnEnemyData.EnemyName.Equals("")) yield break;
        MatchController.Instance.LoadCreateEnemy(spawnEnemyData.SpawnerIndex, spawnEnemyData.EnemyName);
        yield return null;
    }
}