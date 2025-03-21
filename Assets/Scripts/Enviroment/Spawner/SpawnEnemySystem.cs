using System.Collections;
using System.Text;
using GameSave;
using GameSystem;
using UnityEngine;

public class SpawnEnemySystem : MonoBehaviour
{
    public static SpawnEnemySystem Instance { get; private set; }
    [SerializeField] private SpawnEnemy[] spawnEnemyArr;
    [SerializeField] private RoundEnemyTypeConfig[] enemyConfigs;

    public int spawnerIndex { get; private set; }
    public StringBuilder enemyNameToLoad { get; private set; }
    public StringBuilder enemyNameToSave { get; private set; }
    public int enemyNumber { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        enemyNameToSave = new StringBuilder();
        enemyNameToLoad = new StringBuilder();
        spawnerIndex = 0;
        enemyNumber = 1;
    }

    public IEnumerator LoadSpawnEnemy(SpawnEnemyData spawnProgress)
    {
        if (spawnProgress == null)
            yield break;

        spawnerIndex = spawnProgress.SpawnerIndex;
        enemyNameToLoad.Clear();
        enemyNameToLoad.Append(spawnProgress.EnemyName);
        enemyNumber = spawnProgress.EnemyNumber;
    }

    public IEnumerator RunSpawnUnit(int currentRound)
    {
        foreach (var config in enemyConfigs)
        {
            enemyNameToSave.Append(config.enemyPrefabName);
            if (!config.enemyPrefabName.Equals(enemyNameToLoad.ToString()) &&
                !enemyNameToLoad.ToString().Equals("")) continue;

            if (currentRound >= config.firstAppearRound)
            {
                var unitCount = CalculateUnitCount(config, currentRound);

                for (; spawnerIndex < spawnEnemyArr.Length;)
                {
                    for (; enemyNumber <= unitCount; enemyNumber++)
                    {
                        if (PauseSystem.isPausing) yield return new WaitUntil(() => !PauseSystem.isPausing);

                        yield return spawnEnemyArr[spawnerIndex].SpawnUnit(config.enemyPrefabName, currentRound);
                        yield return new WaitForSeconds(1f);
                    }

                    enemyNumber = 1;

                    spawnerIndex++;
                    yield return null;
                }

                spawnerIndex = 0;
            }

            enemyNameToSave.Clear();
        }
    }

    private int CalculateUnitCount(RoundEnemyTypeConfig config, int currentRound)
    {
        if (currentRound == config.firstAppearRound) return config.baseCount;

        var additionalRounds = currentRound - config.firstAppearRound;
        return config.baseCount + additionalRounds * config.incrementPerRound;
    }
}