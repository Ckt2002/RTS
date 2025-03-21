using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data;
using GameSave;
using GameSystem;
using UnityEngine;

public class ObjectDieStatus : MonoBehaviour
{
    [SerializeField] private ObjectSound sound;
    [SerializeField] private List<Renderer> renderers;
    [SerializeField] private float resetTime;

    public bool runExplodeAndSound { get; private set; } = true;
    public float particleElapsedTime { get; private set; }
    public float resetElapsedTime { get; private set; }
    private ParticleSystem particle;
    private string particleName;
    private Coroutine resetCoroutine;

    private void Start()
    {
        if (gameObject.CompareTag(nameof(Tags.EnemyUnit)) || gameObject.CompareTag(nameof(Tags.PlayerUnit)))
            particleName = Names.UnitParticle;
        else
            particleName = Names.BuildingParticle;
    }

    public void PlaySoundAndParticle(ObjectInfor objectStat = null)
    {
        if (runExplodeAndSound)
        {
            if (particle == null)
            {
                particle = ParticlePooling.Instance.GetObjectPool(particleName).GetComponent<ParticleSystem>();
                particle.transform.position = transform.position;
                particle.gameObject.SetActive(true);
                particle.Play();
                particle.time = particleElapsedTime;
            }

            if (sound != null)
                sound.PlayDieSound();

            runExplodeAndSound = false;
        }

        DarkRenderer();

        // For load game
        if (particle == null)
        {
            particle = ParticlePooling.Instance.GetObjectPool(particleName).GetComponent<ParticleSystem>();
            particle.transform.position = transform.position;
            particle.gameObject.SetActive(true);
            particle.time = particleElapsedTime;
            particle.Play();
        }

        if (resetCoroutine == null) Debug.Log("resetCoroutine is null");

        resetCoroutine ??= StartCoroutine(DelayedReset(objectStat, resetTime, particle));
    }

    public void DarkRenderer()
    {
        foreach (var objectRenderer in renderers)
        {
            var originalColor = objectRenderer.material.color;
            var darkerColor = originalColor * 0.5f;
            objectRenderer.material.color = darkerColor;
        }
    }

    private IEnumerator DelayedReset(ObjectInfor objectStat, float delay, ParticleSystem particle)
    {
        var particleDuration = particle.main.duration;

        while (particleElapsedTime < particleDuration)
        {
            if (PauseSystem.isPausing)
            {
                particle.Pause();
                yield return new WaitWhile(() => PauseSystem.isPausing);
                particle.Play();
            }

            particleElapsedTime += Time.deltaTime;
            yield return null;
        }

        particle.Stop();
        particle.gameObject.SetActive(false);

        resetElapsedTime = 0f;
        while (resetElapsedTime < delay)
        {
            if (PauseSystem.isPausing) yield return new WaitWhile(() => PauseSystem.isPausing); // Chờ đợi hết pause

            resetElapsedTime += Time.deltaTime;
            yield return null;
        }

        resetElapsedTime = 0f;
        particleElapsedTime = 0f;

        ResetStatus(objectStat);
    }

    public void ResetStatus(ObjectInfor objectStat)
    {
        Debug.Log("Resetting");
        objectStat.ResetStat();

        foreach (var unitRenderer in renderers)
        {
            var originalColor = unitRenderer.material.color;
            var resetColor = originalColor / 0.5f;
            unitRenderer.material.color = resetColor;
        }

        particle = null;
        runExplodeAndSound = false;
        resetCoroutine = null;
        gameObject.SetActive(false);
    }

    public ObjectDieData SaveProgress()
    {
        var data = new ObjectDieData
        {
            ParticleElapsedTime = particleElapsedTime,
            ResetElapsedTime = resetElapsedTime,
            RunExplodeAndSound = runExplodeAndSound
        };

        return data;
    }

    public void LoadProgress(ObjectDieData dieData)
    {
        runExplodeAndSound = dieData.RunExplodeAndSound;
        particleElapsedTime = dieData.ParticleElapsedTime;
        resetElapsedTime = dieData.ResetElapsedTime;
    }
}