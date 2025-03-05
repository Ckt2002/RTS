using UnityEngine;

public class TestStatic : MonoBehaviour
{
    public static TestStatic Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}