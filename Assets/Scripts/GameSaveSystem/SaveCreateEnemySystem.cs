using GameSave;

public class SaveCreateEnemySystem
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