using System.Collections.Generic;
using GameSystem;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private BuildingInfor stat;
    [SerializeField] private ObjectDieStatus dieStatus;
    [SerializeField] private List<Renderer> childrenRenderer;
    [SerializeField] private PlayerObjectVision vision;

    private void Update()
    {
        if (PauseSystem.isPausing) return;

        if (!stat.IsAlive())
        {
            DieStatusCalculator();
            return;
        }

        vision.SensorEnemy();
    }

    public void SetBuildingTransparent()
    {
        foreach (var child in childrenRenderer)
        {
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
            var mat = child.GetComponent<Renderer>().material;
            mat.shader = Shader.Find("Legacy Shaders/Diffuse");
            var color = mat.color;
            color.a = 1;
            mat.color = color;
        }
    }

    private void DieStatusCalculator()
    {
        dieStatus.PlaySoundAndParticle();
        dieStatus.DarkRenderer();
        dieStatus.ResetCalculator();
        dieStatus.ResetStatus(stat);
    }
}