using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private UnitPooling unitPooling;

    public IEnumerator SpawnUnit(string enemyName, int currentRound)
    {
        var enemy = unitPooling.GetObjectPool(enemyName);

        if (enemy == null)
            yield break;

        enemy.transform.position = transform.position;
        enemy.SetActive(true);
        // enemy.GetComponent<EnemyVisible>().Reset();

        yield return null;
    }
}