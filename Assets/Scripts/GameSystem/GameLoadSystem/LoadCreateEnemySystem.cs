using System.Threading.Tasks;
using GameSave;

public static class LoadCreateEnemySystem
{
    public static Task LoadCreateEnemy(SpawnEnemyData spawnEnemyData)
    {
        if (spawnEnemyData == null) return Task.CompletedTask;

        if (spawnEnemyData.EnemyName.Equals("")) return Task.CompletedTask;
        // MatchController.Instance.LoadCreateEnemy(spawnEnemyData.SpawnerIndex, spawnEnemyData.EnemyName);
        return Task.CompletedTask;
    }
}