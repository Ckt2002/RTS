using System.Collections.Generic;
using UnityEngine;

public class EnemyVisible : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> meshRenderers;
    [SerializeField] private GameObject minimapIcon;

    private int playerCombined;

    private void Start()
    {
        CheckInVision();
    }

    public void Reset()
    {
        playerCombined = 0;
    }

    public void CheckInVision()
    {
        foreach (var meshRenderer in meshRenderers) meshRenderer.enabled = playerCombined > 0;
        minimapIcon.SetActive(playerCombined > 0);
    }

    public void NewCombined()
    {
        playerCombined++;
    }

    public void NewUncombined()
    {
        playerCombined--;
    }
}