using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraMovement movement;
    [SerializeField] private CameraRotation rotation;
    [SerializeField] private CameraZoom zoom;

    private void LateUpdate()
    {
        movement.HandleMovement();
    }
}