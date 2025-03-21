using System.Collections.Generic;
using Assets.Scripts.Data;
using GameSystem;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private BuildingInfor stat;
    [SerializeField] private ObjectDieStatus dieStatus;
    [SerializeField] private List<Renderer> childrenRenderer;

    private void Update()
    {
        if (PauseSystem.isPausing) return;

        if (!stat.IsAlive()) DieStatusCalculator();
    }

    public void SetBuildingTransparent()
    {
        foreach (var child in childrenRenderer)
        {
            child.tag = nameof(Tags.Unable);
            var mat = child.GetComponent<Renderer>().material;
            mat.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            var color = mat.color;
            color.a = 0.5f;
            mat.color = color;
        }
    }

    public void SetBuildingOpaque()
    {
        foreach (var child in childrenRenderer)
        {
            child.tag = nameof(Tags.PlayerBuilding);
            var mat = child.GetComponent<Renderer>().material;
            mat.shader = Shader.Find("Legacy Shaders/Diffuse");
            var color = mat.color;
            color.a = 1;
            mat.color = color;
        }

        GetComponent<FogOfWarUnit>().enabled = true;
    }

    protected virtual void DieStatusCalculator()
    {
        dieStatus.PlaySoundAndParticle(stat);
    }
}