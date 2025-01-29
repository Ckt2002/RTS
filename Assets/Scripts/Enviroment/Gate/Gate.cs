using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private float smoothMovement;
    [SerializeField] private Vector3 openPos;
    [SerializeField] private Vector3 closePos;

    public void OpenGate()
    {
        if (transform.localPosition != openPos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, openPos, smoothMovement * Time.deltaTime);
        }
    }

    public void CloseGate()
    {
        if (transform.localPosition != closePos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, closePos, smoothMovement * Time.deltaTime);
        }
    }
}
