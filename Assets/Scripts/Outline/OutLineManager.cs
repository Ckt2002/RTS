using System.Collections.Generic;
using UnityEngine;

public class AdvancedOutlineManager : MonoBehaviour
{
    [Header("Selection Settings")] public LayerMask selectableLayers;

    public Camera gameCamera;
    public float maxSelectDistance = 100f;

    [Header("Outline Settings")] public Material outlineMaterial;
    private readonly Dictionary<Renderer, Material[]> originalMaterials = new();

    private GameObject currentSelection;

    private void Start()
    {
        if (gameCamera == null)
            gameCamera = Camera.main;

        if (outlineMaterial == null) outlineMaterial = new Material(Shader.Find("Custom/OutlineOnly"));
    }

    private void Update()
    {
        var ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxSelectDistance, selectableLayers))
        {
            var hitObject = hit.collider.gameObject;

            // Only change outline if selecting a different object
            if (currentSelection != hitObject)
            {
                if (currentSelection != null) RemoveOutline();

                currentSelection = hitObject;
                ApplyOutline(currentSelection);
            }
        }
        else if (currentSelection != null)
        {
            RemoveOutline();
            currentSelection = null;
        }
    }

    private void ApplyOutline(GameObject obj)
    {
        // Clear previous data
        originalMaterials.Clear();

        // Get all renderers in the object and its children
        var renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (var renderer in renderers)
        {
            if (renderer.gameObject.name.Equals("Ring")) continue;

            // Store original materials
            originalMaterials[renderer] = renderer.materials;

            // Create new materials array with outline material
            var materials = renderer.materials;
            var newMaterials = new Material[materials.Length + 1];

            // Copy original materials
            for (var i = 0; i < materials.Length; i++) newMaterials[i] = materials[i];

            // Add outline material at the end
            newMaterials[materials.Length] = outlineMaterial;

            // Apply new materials
            renderer.materials = newMaterials;
        }
    }

    private void RemoveOutline()
    {
        // Restore original materials to all renderers
        foreach (var pair in originalMaterials)
            if (pair.Key != null)
                pair.Key.materials = pair.Value;

        originalMaterials.Clear();
    }
}