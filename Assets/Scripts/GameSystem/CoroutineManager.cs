using System.Collections;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public Coroutine StartManagedCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public void StopManagedCoroutine(Coroutine coroutine)
    {
        if (coroutine != null) StopCoroutine(coroutine);
    }
}