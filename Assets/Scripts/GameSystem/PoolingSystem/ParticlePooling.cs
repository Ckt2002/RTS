using System;
using System.Collections;
using UnityEngine;

public class ParticlePooling : ObjectPool
{
    public static ParticlePooling Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    protected override IEnumerator SpawnObjects(Action action)
    {
        yield return base.SpawnObjects(action);

        CallBackAction(action);
    }

    public void ReturnParticle(ParticleSystem particle)
    {
        particle.Stop();
        particle.gameObject.SetActive(false);
    }
}