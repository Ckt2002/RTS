using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSystem;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    public static MatchController Instance;

    [SerializeField] private float timeToNextLevel;
    [SerializeField] private int maxRound;
    private readonly int enemyNumber = 1;

    private int round;
    private List<SpawnEnemy> spawnEnemiesLst;

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
            CoroutineManager.Instance.StartManagedCoroutine(CreateEnemy(spawnEnemiesLst));
    }

    private IEnumerator CreateEnemy(List<SpawnEnemy> spawnEnemiesLst)
    {
        var enemyUnitNames = Names.enemyUnitNameLst;

        yield return new WaitForSeconds(1f);

        while (round <= maxRound)
        {
            var timer = 0f;
            var nameInd = 0;

            foreach (var spawnEnemy in spawnEnemiesLst)
            {
                while (nameInd <= round)
                {
                    var i = 0;
                    for (; i < enemyNumber;)
                    {
                        if (PauseSystem.isPausing)
                        {
                            SaveMatchSystem.GetMatchData(round, timer, SaveCreateEnemySystem.SaveCreateEnemyProgress(
                                spawnEnemiesLst.IndexOf(spawnEnemy),
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

            while (timer < timeToNextLevel)
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