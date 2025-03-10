using Assets.Scripts.Data;
using UnityEngine;

public abstract class UnitBullet : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float lifetime = 30f;
    [SerializeField] protected Tags targetBuildingTag;
    [SerializeField] protected Tags targetUnitTag;

    private float timer;

    public int Damage { get; private set; }

    public void SetupDamage(int damage)
    {
        this.Damage = damage;
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
            if (!hitCollider.CompareTag(targetUnitTag.ToString()) &&
                !hitCollider.CompareTag(targetBuildingTag.ToString()))
                continue;

            var takeDamageComponent = hitCollider.GetComponentInParent<ObjectTakeDamage>();
            HitTarget(takeDamageComponent);
        }
    }

    protected void HitTarget(ObjectTakeDamage target)
    {
        if (target != null)
        {
            target.TakeDamage(Damage);
            Damage = 0;
            gameObject.SetActive(false);
        }
    }
}