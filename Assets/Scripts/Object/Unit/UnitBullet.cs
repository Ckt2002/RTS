using Assets.Scripts.Data;
using UnityEngine;

public abstract class UnitBullet : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float lifetime = 30f;
    protected int damage;
    protected string targetTag = Tags.EnemyUnit;

    protected float timer;

    public void BulletOwner(bool isPlayer)
    {
        if (!isPlayer)
            targetTag = Tags.PlayerUnit;
    }

    public void SetupDamage(int damage)
    {
        this.damage = damage;
    }

    protected void CheckLifetime()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            timer = 0f;
            gameObject.SetActive(false);
        }
    }

    protected virtual void CheckCollision()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (var hitCollider in hitColliders)
        {
            if (!hitCollider.CompareTag(targetTag))
                continue;

            var takeDamageComponent = hitCollider.GetComponentInParent<UnitTakeDamage>();
            HitTarget(takeDamageComponent);
        }
    }

    protected void HitTarget(UnitTakeDamage target)
    {
        if (target != null)
        {
            target.TakeDamage(damage);
            damage = 0;
            gameObject.SetActive(false);
        }
    }
}