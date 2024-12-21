using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private List<Transform> childTransforms;

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
}