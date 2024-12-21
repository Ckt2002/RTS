using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float smooth;
    [SerializeField] private Vector3 minBoundary;
    [SerializeField] private Vector3 maxBoundary;

    private Vector3 newPosition;

    private void Start()
    {
        newPosition = transform.position;
    }

    public void HandleMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            newPosition += transform.forward * speed;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            newPosition += transform.forward * -speed;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            newPosition += transform.right * speed;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            newPosition += transform.right * -speed;

        newPosition.x = Mathf.Clamp(newPosition.x, minBoundary.x, maxBoundary.x);
        newPosition.z = Mathf.Clamp(newPosition.z, minBoundary.z, maxBoundary.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * smooth);
    }
}