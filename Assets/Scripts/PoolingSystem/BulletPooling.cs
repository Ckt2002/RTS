public class BulletPooling : ObjectPool
{
    public static BulletPooling instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}