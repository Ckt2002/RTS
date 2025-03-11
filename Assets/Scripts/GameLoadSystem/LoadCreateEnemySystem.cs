using System.Collections;
using GameSave;
using UnityEngine;

public class LoadCreateEnemySystem : MonoBehaviour
{
    public static IEnumerator LoadCreateEnemy(SpawnEnemyData spawnEnemyData)
    {
        Debug.Log(spawnEnemyData.EnemyName);
        yield return null;
    }
}