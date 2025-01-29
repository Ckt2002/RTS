using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] Gate leftGate;
    [SerializeField] Gate rightGate;

    private int playerUnitInRange = 0;

    private void Update()
    {
        if (playerUnitInRange > 0)
        {
            leftGate.OpenGate();
            rightGate.OpenGate();
        }
        else if (playerUnitInRange <= 0)
        {
            leftGate.CloseGate();
            rightGate.CloseGate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            playerUnitInRange++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            playerUnitInRange--;
        }
    }
}
