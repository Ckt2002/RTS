using GameSave;
using UnityEngine;

public static class SaveMatchSystem
{
    public static MatchData SaveMatchData()
    {
        var matchController = MatchController.Instance;
        var spawnEnemy = SpawnEnemySystem.Instance;

        SpawnEnemyData spawnData = null;
        if (matchController.isSpawning)
            spawnData = new SpawnEnemyData
            {
                SpawnerIndex = spawnEnemy.spawnerIndex,
                EnemyName = spawnEnemy.enemyNameToSave.ToString(),
                EnemyNumber = spawnEnemy.enemyNumber
            };

        var data = new MatchData
        {
            CurrentRound = matchController.currentRound,
            RoundTimer = matchController.timer,
            IsSpawning = matchController.isSpawning,
            SpawnEnemyData = spawnData
        };

        var json = JsonUtility.ToJson(data, true);
        Debug.Log(json);

        return data;
    }
}