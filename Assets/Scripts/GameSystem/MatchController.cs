using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    [SerializeField] private float timeToNextLevel;
    [SerializeField] private int maxGround;
    private readonly int enemyNumber = 1;

    private int ground = 1;
    private List<SpawnEnemy> spawnEnemiesLst;

    private void Start()
    {
        spawnEnemiesLst = FindObjectsOfType<SpawnEnemy>().ToList();
        CoroutineManager.Instance.StartManagedCoroutine(CreateEnemy(spawnEnemiesLst));
    }

    private IEnumerator CreateEnemy(List<SpawnEnemy> spawnEnemiesLst)
    {
        var enemyUnitNames = Names.enemyUnitNameLst;
        var nameInd = 0;

        yield return new WaitForSeconds(10f);

        var HQ = BuildingPooling.Instance.GetObjectPool(Names.PlayerHeadquarter);
        HQ.transform.position = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(30f);
        while (ground <= maxGround)
        {
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