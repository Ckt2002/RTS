using System;

namespace GameSave
{
    [Serializable]
    public class SpawnEnemyData
    {
        public int SpawnerIndex;
        public ObjectData CurrentEnemySpawn;
        public CoroutineProgressData Coroutine;
    }
}