using UnityEngine;

public class AirBullet : UnitBullet
{
    private GameObject target;

    private void Update()
    {
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
            if (target.GetComponent<UnitTakeDamage>() != null)
                HitTarget(target.GetComponent<UnitTakeDamage>());
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}