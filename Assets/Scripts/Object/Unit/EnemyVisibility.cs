using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisibility : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> meshRenderers;
    [SerializeField] private GameObject minimapIcon;
    [SerializeField] private ObjectInfor stat;
    private FogOfWar fogOfWar;

    private void Start()
    {
        fogOfWar = FindObjectOfType<FogOfWar>();

        StartCoroutine(UpdateVisibility());
    }

    private IEnumerator UpdateVisibility()
    {
        while (stat.IsAlive())
        {
            CheckVisible();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void CheckVisible()
    {
        var isVisible = fogOfWar.IsPositionVisible(transform.position);

        foreach (var renderer in meshRenderers) renderer.enabled = isVisible;

        minimapIcon.SetActive(isVisible);
    }
}