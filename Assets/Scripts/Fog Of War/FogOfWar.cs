using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [Header("Fog Settings")] [SerializeField]
    private int resolution = 256;

    [SerializeField] private LayerMask fogLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float updateInterval = 0.2f;
    [SerializeField] private Material fogMaterial;

    [Header("Map Settings")] [SerializeField]
    private float mapSize = 100f;

    [SerializeField] private Transform mapOrigin;

    private readonly List<FogOfWarUnit> visibilityUnits = new();
    private float cellSize;
    private bool[] exploredGrid;
    private Color[] fogColors;
    private bool[] fogGrid;

    private Texture2D fogTexture;

    private void Start()
    {
        Initialize();
        StartCoroutine(UpdateFogCoroutine());
    }

    private void Initialize()
    {
        // Initialize fog texture
        fogTexture = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
        fogTexture.filterMode = FilterMode.Bilinear;
        fogTexture.wrapMode = TextureWrapMode.Clamp;

        // Set fog material texture
        fogMaterial.mainTexture = fogTexture;

        // Initialize fog data
        fogColors = new Color[resolution * resolution];
        fogGrid = new bool[resolution * resolution];
        exploredGrid = new bool[resolution * resolution];

        // Calculate cell size
        cellSize = mapSize / resolution;

        // Initialize all cells to unexplored (black)
        for (var i = 0; i < fogColors.Length; i++) fogColors[i] = Color.black;

        // Apply to texture
        fogTexture.SetPixels(fogColors);
        fogTexture.Apply();
    }

    public void RegisterUnit(FogOfWarUnit unit)
    {
        if (!visibilityUnits.Contains(unit)) visibilityUnits.Add(unit);
    }

    public void UnregisterUnit(FogOfWarUnit unit)
    {
        if (visibilityUnits.Contains(unit)) visibilityUnits.Remove(unit);
    }

    private IEnumerator UpdateFogCoroutine()
    {
        while (true)
        {
            UpdateFogOfWar();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    private void UpdateFogOfWar()
    {
        // Reset current visibility
        for (var i = 0; i < fogGrid.Length; i++) fogGrid[i] = false;

        // Update visibility for all units
        foreach (var unit in visibilityUnits)
            if (unit.gameObject.activeInHierarchy)
                UpdateUnitVisibility(unit);

        // Update texture
        for (var x = 0; x < resolution; x++)
        for (var y = 0; y < resolution; y++)
        {
            var index = y * resolution + x;

            if (fogGrid[index])
            {
                // Visible area (clear)
                fogColors[index] = new Color(0, 0, 0, 0);
                exploredGrid[index] = true;
            }
            else if (exploredGrid[index])
            {
                // Explored but not visible (semi-transparent)
                fogColors[index] = new Color(0, 0, 0, 0.5f);
            }
            else
            {
                // Unexplored (opaque)
                fogColors[index] = Color.black;
            }
        }

        // Apply changes to texture
        fogTexture.SetPixels(fogColors);
        fogTexture.Apply();
    }

    private void UpdateUnitVisibility(FogOfWarUnit unit)
    {
        var unitPosition = unit.transform.position;
        var viewDistance = unit.ViewDistance;

        // Convert to grid coordinates
        var gridPos = WorldToGrid(unitPosition);
        var viewRadius = Mathf.CeilToInt(viewDistance / cellSize);

        // Determine visibility using raycast
        for (var x = gridPos.x - viewRadius; x <= gridPos.x + viewRadius; x++)
        for (var y = gridPos.y - viewRadius; y <= gridPos.y + viewRadius; y++)
        {
            // Skip if out of bounds
            if (x < 0 || y < 0 || x >= resolution || y >= resolution)
                continue;

            var index = y * resolution + x;

            // Skip if already visible
            if (fogGrid[index])
                continue;

            // Skip if outside view radius
            var offset = new Vector2Int(x - gridPos.x, y - gridPos.y);
            if (offset.sqrMagnitude > viewRadius * viewRadius)
                continue;

            // Check if line of sight is clear
            var targetWorldPos = GridToWorld(new Vector2Int(x, y));
            var direction = targetWorldPos - unitPosition;
            direction.y = 0; // Ignore height difference

            if (direction.magnitude <= viewDistance)
                // Cast ray to check for obstacles
                if (!Physics.Raycast(unitPosition + Vector3.up * 1.0f, direction.normalized,
                        direction.magnitude, obstacleLayer))
                    fogGrid[index] = true;
        }
    }

    private Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        // Convert world position to grid coordinates
        var relativePos = worldPosition - mapOrigin.position + new Vector3(mapSize / 2, 0, mapSize / 2);
        var x = Mathf.FloorToInt(relativePos.x / cellSize);
        var y = Mathf.FloorToInt(relativePos.z / cellSize);

        return new Vector2Int(
            Mathf.Clamp(x, 0, resolution - 1),
            Mathf.Clamp(y, 0, resolution - 1)
        );
    }

    private Vector3 GridToWorld(Vector2Int gridPosition)
    {
        // Convert grid coordinates to world position
        var x = gridPosition.x * cellSize + cellSize / 2;
        var z = gridPosition.y * cellSize + cellSize / 2;

        return mapOrigin.position + new Vector3(x - mapSize / 2, 0, z - mapSize / 2);
    }
}