using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSystem;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    public static MatchController Instance;

    [SerializeField] private float timeToNextLevel;
    [SerializeField] private int maxGround;
    private readonly int enemyNumber = 1;

    private int ground = 1;
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
        var nameInd = 0;

        yield return new WaitForSeconds(10f);

        // yield return new WaitForSeconds(30f);
        while (ground <= maxGround)
        {
            if (PauseSystem.isPausing)
            {
                Debug.Log("Coroutine Paused");
                yield return null;
                continue;
            }

            foreach (var spawnEnemy in spawnEnemiesLst)
                while (nameInd <= ground)
                {
                    for (var i = 0; i < enemyNumber; i++)
                        spawnEnemy.SpawnUnit(enemyUnitNames[nameInd]);
                    nameInd++;
                }

            yield return new WaitForSeconds(timeToNextLevel);
            ground++;
        }
    }
}