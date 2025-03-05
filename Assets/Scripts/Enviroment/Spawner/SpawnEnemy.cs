using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private UnitPooling unitPooling;

    public void SpawnUnit(string enemyName)
    {
        var enemy = unitPooling.GetObjectPool(enemyName);

        if (enemy == null)
            return;

        enemy.transform.position = transform.position;
        enemy.SetActive(true);
        enemy.GetComponent<EnemyVisible>().Reset();
    }
}