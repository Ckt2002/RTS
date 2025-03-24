using System.Collections;
using GameSave;
using GameSystem;
using TMPro;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    public static MatchController Instance { get; private set; }

    [SerializeField] private TMP_Text timeAndRoundText;
    [SerializeField] private float timeToNextRound;
    [SerializeField] private int maxRound;

    private bool isLoading;
    public float timer { get; private set; }
    public int currentRound { get; private set; }
    public bool isSpawning { get; private set; }
    public bool waitingForNextRound { get; private set; }
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
        waitingForNextRound = false;
        timeAndRoundText.text = FormatTime(timer);
    }

    public void RunMatch()
    {
        StartCoroutine(FogOfWar.Instance.UpdateFogCoroutine());
        mathCoroutine ??= StartCoroutine(RoundCoroutine());
    }

    private IEnumerator RoundCoroutine()
    {
        while (currentRound < maxRound)
        {
            if (PauseSystem.isPausing) yield return new WaitUntil(() => !PauseSystem.isPausing);

            yield return TimerCoroutine();
        }

        if (currentRound == maxRound) GameResult.GameComplete();
    }

    private IEnumerator TimerCoroutine()
    {
        if ((!isSpawning && waitingForNextRound && isLoading) || (!isLoading && !waitingForNextRound))
        {
            waitingForNextRound = true;
            while (timer >= 0)
            {
                if (PauseSystem.isPausing) yield return new WaitUntil(() => !PauseSystem.isPausing);

                timer -= Time.deltaTime;
                timeAndRoundText.text = FormatTime(timer);
                yield return null;
            }

            waitingForNextRound = false;
            currentRound++;
            timeAndRoundText.text = SetRoundText(currentRound);
        }
        // Nếu dữ liệu lưu đang đến đoạn spawn unit
        else if (isLoading && isSpawning)
        {
            yield return SpawnEnemySystem.Instance.LoadSpawnEnemy(spawnProgress);
            spawnProgress = null;
        }


        // Không load game, chạy bình thường
        if (!isLoading)
        {
            isSpawning = true;
            yield return SpawnEnemySystem.Instance.RunSpawnUnit(currentRound);
            isSpawning = false;
        }

        yield return new WaitUntil(() => !CheckEnemyAlive());
        timer = timeToNextRound;
        timeAndRoundText.text = FormatTime(timer);
        isLoading = false;
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

    public void LoadMatch(int currentRound, float timer, bool isSpawning, bool waitingForNextRound,
        SpawnEnemyData spawnProgress)
    {
        if (mathCoroutine != null)
            StopCoroutine(mathCoroutine);

        this.currentRound = currentRound;
        this.timer = timer;
        this.isSpawning = isSpawning;
        this.spawnProgress = spawnProgress;
        this.waitingForNextRound = waitingForNextRound;
        isLoading = true;

        timeAndRoundText.text = timer > 0 ? SetTimeText(timer) : SetRoundText(currentRound);

        mathCoroutine ??= StartCoroutine(RoundCoroutine());
    }

    private string SetRoundText(int currentRound)
    {
        return $"Round {currentRound}";
    }

    private string SetTimeText(float timer)
    {
        return FormatTime(timer);
    }
}