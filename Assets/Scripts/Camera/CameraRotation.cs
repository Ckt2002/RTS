using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float speed;
    [SerializeField] private float smooth;

    private Quaternion newRotation;
    private Quaternion newCamRotation;
    private Vector3 mouseStartPos;
    private Vector3 mouseCurrentPos;

    private void Start()
    {
        mouseStartPos = mouseCurrentPos = Input.mousePosition;
        newRotation = transform.rotation;
        newCamRotation = cam.rotation;
    }

    public void HandleRotation()
    {
        RotateByMouse();
        RotateByKeyBoard();
    }

    private void RotateByMouse()
    {
        if (Input.GetMouseButtonDown(2))
            mouseStartPos = Input.mousePosition;

        if (Input.GetMouseButton(2))
        {
            mouseCurrentPos = Input.mousePosition;

            Vector3 drag = mouseCurrentPos - mouseStartPos;
            mouseStartPos = mouseCurrentPos;

            if (Mathf.Abs(drag.y) > Mathf.Abs(drag.x))
            {
                float rotationX = -drag.y * Time.deltaTime * smooth;

                float camXAngle = cam.localEulerAngles.x;
                camXAngle = Mathf.Clamp(cam.localEulerAngles.x + rotationX, 0, 55);

                cam.localRotation = Quaternion.Euler(camXAngle, cam.localEulerAngles.y, cam.localEulerAngles.z);
            }
            else
                newRotation *= Quaternion.Euler(Vector3.up * drag.x * Time.deltaTime * smooth);
        }
    }

    private void RotateByKeyBoard()
    {
        if (Input.GetKey(KeyCode.Q))
            newRotation *= Quaternion.Euler(Vector3.up * -speed);

        if (Input.GetKey(KeyCode.E))
            newRotation *= Quaternion.Euler(Vector3.up * speed);

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * smooth);
    }
}
