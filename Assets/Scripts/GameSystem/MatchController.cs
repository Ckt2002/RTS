using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSystem;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    public static MatchController Instance;

    [SerializeField] private float timeToNextRound;

    [SerializeField] private int maxRound;
    private readonly int enemyNumber = 1;

    private int round;
    private float timer;
    private List<SpawnEnemy> spawnEnemiesLst;

    private Coroutine createEnemyCoroutine;
    private int spawnEnemyInd;
    private string spawnEnemyName = "";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        spawnEnemiesLst = FindObjectsOfType<SpawnEnemy>().ToList();
    }

    public void StartMatch()
    {
        if (CoroutineManager.Instance != null)
            CoroutineManager.Instance.StartManagedCoroutine(CreateEnemy(spawnEnemiesLst, spawnEnemyInd,
                spawnEnemyName));
    }

    public void StopMatch()
    {
        if (CoroutineManager.Instance != null)
            CoroutineManager.Instance.StopManagedCoroutine(createEnemyCoroutine);
    }

    public void LoadMatch(int currentRound, float currentTimeToNextRound)
    {
        round = currentRound;
        timer = currentTimeToNextRound;
    }

    public void LoadCreateEnemy(int spawnInd = 0, string enemyName = "")
    {
        spawnEnemyInd = spawnInd;
        spawnEnemyName = enemyName;
    }

    private IEnumerator CreateEnemy(List<SpawnEnemy> spawnEnemiesLst, int spawnInd = 0, string enemyName = "")
    {
        var enemyUnitNames = Names.enemyUnitNameLst;

        yield return new WaitForSeconds(1f);

        while (round <= maxRound)
        {
            var nameInd = 0;

            for (var spawnIndex = 0; spawnIndex < spawnEnemiesLst.Count; spawnIndex++)
            {
                if (!string.IsNullOrEmpty(enemyName) && spawnIndex < spawnInd) continue;

                var spawnEnemy = spawnEnemiesLst[spawnIndex];
                while (nameInd <= round)
                {
                    var i = 0;

                    if (!string.IsNullOrEmpty(enemyName) && spawnIndex == spawnInd)
                    {
                        var enemyNameIndex = enemyUnitNames.IndexOf(enemyName);
                        if (enemyNameIndex >= 0)
                        {
                            nameInd = enemyNameIndex;
                            enemyName = "";
                        }
                    }

                    for (; i < enemyNumber;)
                    {
                        if (PauseSystem.isPausing)
                        {
                            SaveMatchSystem.GetMatchData(round, timer, SaveCreateEnemySystem.SaveCreateEnemyProgress(
                                spawnIndex,
                                enemyUnitNames[nameInd]));
                            yield return null;
                            continue;
                        }

                        i++;
                        spawnEnemy.SpawnUnit(enemyUnitNames[nameInd]);
                        yield return null;
                    }

                    nameInd++;
                    yield return null;
                }

                yield return null;
            }

            while (timer < timeToNextRound)
            {
                if (PauseSystem.isPausing)
                {
                    SaveMatchSystem.GetMatchData(round, timer, null);
                    yield return null;
                    continue;
                }

                timer += Time.deltaTime;
                yield return null;
            }

            round++;
        }
    }
}