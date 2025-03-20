using System.Collections;
using GameSave;
using GameSystem;
using TMPro;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    public static MatchController Instance { get; private set; }

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text roundText;
    [SerializeField] private float timeToNextRound;
    [SerializeField] private int maxRound;

    public float timer { get; private set; }
    public int currentRound { get; private set; }
    public bool isSpawning { get; private set; }
    private Coroutine mathCoroutine;
    private SpawnEnemyData spawnProgress;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        timer = timeToNextRound;
        timeText.text = FormatTime(timer);
        roundText.text = currentRound.ToString();
    }

    public void RunMatch()
    {
        mathCoroutine ??= StartCoroutine(RoundCoroutine());
    }

    private IEnumerator RoundCoroutine()
    {
        while (currentRound <= maxRound)
        {
            if (PauseSystem.isPausing) yield return new WaitUntil(() => !PauseSystem.isPausing);

            yield return TimerCoroutine();
            timer = timeToNextRound;
        }
    }

    private IEnumerator TimerCoroutine()
    {
        if (!isSpawning)
        {
            while (timer >= 0)
            {
                if (PauseSystem.isPausing) yield return new WaitUntil(() => !PauseSystem.isPausing);

                timer -= Time.deltaTime;
                timeText.text = FormatTime(timer);
                yield return null;
            }

            currentRound++;
            roundText.text = currentRound.ToString();
        }
        else
        {
            yield return SpawnEnemySystem.Instance.LoadSpawnEnemy(spawnProgress);
            spawnProgress = null;
        }

        isSpawning = true;
        yield return SpawnEnemySystem.Instance.RunSpawnUnit(currentRound);
        isSpawning = false;

        yield return new WaitUntil(() => !CheckEnemyAlive());
        Debug.Log("All enemy destroyed");
    }

    private bool CheckEnemyAlive()
    {
        var countActive = 0;
        var enemies = UnitPooling.Instance.GetParent(1).GetComponentsInChildren<EnemyUnitController>(true);
        foreach (var enemy in enemies)
        {
            if (!enemy.gameObject.activeInHierarchy) continue;
            countActive++;
        }

        return countActive > 0;
    }

    private string FormatTime(float timeInSeconds)
    {
        timeInSeconds = Mathf.Max(0, timeInSeconds);
        var minutes = Mathf.FloorToInt(timeInSeconds / 60);
        var seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return $"{minutes:00}:{seconds:00}";
    }

    public void LoadMatch(int currentRound, float timer, bool isSpawning, SpawnEnemyData spawnProgress)
    {
        if (mathCoroutine != null)
            StopCoroutine(mathCoroutine);

        this.currentRound = currentRound;
        this.timer = timer;
        this.isSpawning = isSpawning;
        this.spawnProgress = spawnProgress;

        timeText.text = FormatTime(timer);
        roundText.text = currentRound.ToString();

        mathCoroutine ??= StartCoroutine(RoundCoroutine());
    }
}