using UnityEngine;

public class GroundBullet : UnitBullet
{
    private void Update()
    {
        CheckLifetime();
        var transform1 = transform;
        transform1.position += speed * Time.deltaTime * transform1.forward;
        CheckCollision();
    }
}