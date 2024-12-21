using UnityEngine;

public class PlayerRing : MonoBehaviour
{
    [SerializeField] private GameObject unitRing;
    [SerializeField] private Vector3 lineRendererOffset = new(0, 0.3f, 0);
    [SerializeField] private LineRenderer lineRenderer;

    public bool isSelected { get; private set; }

    private void Awake()
    {
        lineRenderer.SetPosition(0, transform.position + lineRendererOffset);
        lineRenderer.SetPosition(1, transform.position + lineRendererOffset);
    }

    private void Update()
    {
        if (lineRenderer)
            lineRenderer.SetPosition(0, transform.position + lineRendererOffset);
    }

    public void UnitSelected()
    {
        unitRing.SetActive(isSelected = true);
    }

    public void UnitDeselected()
    {
        unitRing.SetActive(isSelected = false);
    }

    public void ShowLine(Vector3 movePos)
    {
        if (lineRenderer)
            lineRenderer.SetPosition(1, movePos + lineRendererOffset);
    }
}