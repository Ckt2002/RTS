using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float speed;
    [SerializeField] private float smooth;

    [SerializeField] private Vector3 zoomAmount;
    private Vector3 newZoom;

    private void Start()
    {
        newZoom = cam.localPosition;
    }

    public void HandleZoom()
    {
        if (PlaceBuildingSystem.Instance.IsPlacingBuilding)
            return;

        var scrollData = Input.GetAxis("Mouse ScrollWheel");
        newZoom.y -= speed * scrollData * 10;
        newZoom.y = Mathf.Clamp(newZoom.y, -41f, 70f);
        cam.localPosition = Vector3.Lerp(cam.localPosition, newZoom, Time.deltaTime * smooth);
    }
}