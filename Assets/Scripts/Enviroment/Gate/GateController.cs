using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private Gate leftGate;
    [SerializeField] private Gate rightGate;

    private int playerUnitInRange;

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
        if (other.gameObject.name.Contains(Names.Player)) playerUnitInRange++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains(Names.Player)) playerUnitInRange--;
    }
}