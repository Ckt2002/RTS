public class UnitPooling : ObjectPool
{
    public static UnitPooling Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}