using GameSystem;
using UnityEngine;

public class AirBullet : UnitBullet
{
    private GameObject target;

    private void Update()
    {
        if (PauseSystem.isPausing)
        {
            Debug.Log("Paused");
            return;
        }

        if (target != null)
        {
            var direction = (target.transform.position - transform.position).normalized;

            transform.position += direction * (speed * Time.deltaTime);

            CheckCollision();
        }
    }

    protected override void CheckCollision()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            if (target.GetComponent<ObjectTakeDamage>() != null)
                HitTarget(target.GetComponent<ObjectTakeDamage>());
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}