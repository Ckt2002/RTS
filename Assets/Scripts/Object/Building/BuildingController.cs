using Assets.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private List<Transform> childTransforms;
    [SerializeField] private List<Renderer> renderers;
    [SerializeField] private BuildingInfor buildingInfor;


    private BuildingManager buildingManager;
    private readonly float timeToHideAfterDied = 2f;
    private float deathTimer;

    private void Start()
    {
        buildingManager = BuildingManager.Instance;
    }

    private void Update()
    {
        if (buildingInfor.CurrentHealth <= 0)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= timeToHideAfterDied)
                ResetStatus();

            return;
        }

        if (buildingInfor.CurrentHealth > 0)
            return;

        buildingManager.BuildingsOnMap.Remove(this.GetComponent<ObjectInfor>());

        if (CompareTag(Tags.PlayerUnit))
            transform.GetComponent<PlayerRing>().UnitDeselected();

        foreach (var unitRenderer in renderers)
        {
            var originalColor = unitRenderer.material.color;
            var darkerColor = originalColor * 0.5f;
            unitRenderer.material.color = darkerColor;
        }
    }

    public void SetBuildingTransparent()
    {
        foreach (var child in childTransforms)
        {
            var mat = child.GetComponent<Renderer>().material;
            mat.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            var color = mat.color;
            color.a = 0.5f;
            mat.color = color;
        }
    }

    public void SetBuldingOpaque()
    {
        foreach (var child in childTransforms)
        {
            var mat = child.GetComponent<Renderer>().material;
            mat.shader = Shader.Find("Legacy Shaders/Diffuse");
            var color = mat.color;
            color.a = 1;
            mat.color = color;
        }
    }

    private void ResetStatus()
    {
        deathTimer = 0f;
        buildingInfor.ResetStat();

        foreach (var unitRenderer in renderers)
        {
            var originalColor = unitRenderer.material.color;
            var resetColor = originalColor / 0.5f;
            unitRenderer.material.color = resetColor;
        }
        gameObject.SetActive(true);
    }
}