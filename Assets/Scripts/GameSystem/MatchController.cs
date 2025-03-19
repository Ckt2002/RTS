using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSystem;
using TMPro;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    public static MatchController Instance;

    private TMP_Text timeText;
    private TMP_Text roundText;

    [SerializeField] private float timeToNextRound;
    [SerializeField] private int maxRound;
    [SerializeField] private int enemyNumberEachType = 1;

    private int round;
    private float timer;
    private List<SpawnEnemy> spawnEnemiesLst;

    private Coroutine createEnemyCoroutine;
    private int spawnEnemyInd;
    private string spawnEnemyName = "";

    private bool isInMatch;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        timer = 10f;
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

    public void LoadMatch(int currentRound, float currentTimeToNextRound, bool isInMatch)
    {
        round = currentRound;
        timer = currentTimeToNextRound;
        this.isInMatch = isInMatch;
    }

    public void LoadCreateEnemy(int spawnInd = 0, string enemyName = "")
    {
        spawnEnemyInd = spawnInd;
        spawnEnemyName = enemyName;
    }

    private IEnumerator CreateEnemy(List<SpawnEnemy> spawnEnemiesLst, int spawnInd = 0, string enemyName = "")
    {
        var enemyUnitNames = Names.enemyUnitNameLst;

        // Case 1
        if (!isInMatch)
            yield return RunTimer();

        // Case 2
        while (round <= maxRound)
        {
            roundText.text = round.ToString();
            var nameInd = 0;

            for (var spawnIndex = 0; spawnIndex < spawnEnemiesLst.Count; spawnIndex++)
            {
                if (isInMatch && spawnEnemyName.Equals("")) yield break;

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

                    for (; i < enemyNumberEachType;)
                    {
                        if (PauseSystem.isPausing)
                        {
                            SaveMatchSystem.GetMatchData(round, timer, true,
                                SaveCreateEnemySystem.SaveCreateEnemyProgress(
                                    spawnIndex,
                                    enemyUnitNames[nameInd]));
                            yield return null;
                            continue;
                        }

                        i++;
                        spawnEnemy.SpawnUnit(enemyUnitNames[nameInd]);
                        yield return new WaitForSeconds(2f);
                    }

                    nameInd++;
                    yield return new WaitForSeconds(30f);
                }

                yield return null;
            }

            // Case 3
            yield return RunTimer();

            timer = timeToNextRound;

            round++;
        }
    }

    private IEnumerator RunTimer()
    {
        while (timer >= 0)
        {
            if (PauseSystem.isPausing)
            {
                SaveMatchSystem.GetMatchData(round, timer, false, null);
                yield return null;
                continue;
            }

            timer -= Time.deltaTime;

            var minutes = Mathf.Max(0, Mathf.FloorToInt(timer / 60f));
            var seconds = Mathf.Max(0, Mathf.FloorToInt(timer % 60f));
            timeText.text = $"{minutes:00}:{seconds:00}";

            yield return null;
        }
    }
}