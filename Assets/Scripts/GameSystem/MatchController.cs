using UnityEngine;

public class MatchController : MonoBehaviour
{
    public static MatchController Instance;

    //[SerializeField] float timeToNextRound = 5;
    //[SerializeField] float timeOffset = 5;
    public int level = 1;
    public float counter = 0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
        counter = level * 60;
    }

    void Update()
    {
        counter -= Time.deltaTime;

        if (counter <= 0)
        {
            Debug.Log("Next Round");
            level++;
            counter = 60 * level;
        }
    }
}
