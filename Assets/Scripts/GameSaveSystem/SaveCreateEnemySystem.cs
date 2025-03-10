using GameSave;
using UnityEngine;

public class SaveCreateEnemySystem : MonoBehaviour
{
    public static SpawnEnemyData SaveCreateEnemyProgress(int spawnPosIndex, string currentEnemySpawnName)
    {
        return new SpawnEnemyData
        {
            SpawnerIndex = spawnPosIndex,
            EnemyName = currentEnemySpawnName
        };
    }
}