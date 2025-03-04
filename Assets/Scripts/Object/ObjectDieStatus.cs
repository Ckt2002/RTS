using System.Collections.Generic;
using UnityEngine;

public class ObjectDieStatus : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private ObjectSound sound;
    [SerializeField] private List<Renderer> renderers;
    [SerializeField] private float resetTime;

    private float counter;
    private bool runExplodeAndSound;

    public List<Renderer> GetRenderer => renderers;

    public void PlaySoundAndParticle()
    {
        if (!runExplodeAndSound)
            return;

        if (explosionEffect != null)
            explosionEffect.Play();
        if (sound != null)
            sound.PlayDieSound();

        runExplodeAndSound = true;
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

    public void ResetCalculator()
    {
        counter += Time.deltaTime;
    }

    public void ResetStatus(ObjectInfor objectStat)
    {
        if (counter >= resetTime)
            return;

        counter = 0;
        objectStat.ResetStat();

        foreach (var unitRenderer in renderers)
        {
            var originalColor = unitRenderer.material.color;
            var resetColor = originalColor / 0.5f;
            unitRenderer.material.color = resetColor;
        }

        runExplodeAndSound = false;
        gameObject.SetActive(false);
    }
}