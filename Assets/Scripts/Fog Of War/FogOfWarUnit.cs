using UnityEngine;

public class FogOfWarUnit : MonoBehaviour
{
    [SerializeField] private float viewDistance = 10f;

    private FogOfWar fogOfWar;
    public float ViewDistance => viewDistance;

    private void Start()
    {
        fogOfWar = FindObjectOfType<FogOfWar>();
        if (fogOfWar != null) fogOfWar.RegisterUnit(this);
    }

    private void OnEnable()
    {
        if (fogOfWar != null) fogOfWar.RegisterUnit(this);
    }

    private void OnDisable()
    {
        if (fogOfWar != null) fogOfWar.UnregisterUnit(this);
    }
}