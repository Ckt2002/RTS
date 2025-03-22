using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapturePointVisibility : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> meshRenderers;
    [SerializeField] private Image image;
    [SerializeField] private GameObject minimapIcon;
    private FogOfWar fogOfWar;
    private Coroutine checkVisibilityCoroutine;

    private void Start()
    {
        fogOfWar = FindObjectOfType<FogOfWar>();

        checkVisibilityCoroutine = StartCoroutine(UpdateVisibility());
    }

    private IEnumerator UpdateVisibility()
    {
        while (true)
        {
            var isVisible = fogOfWar.IsPositionVisible(transform.position);

            foreach (var renderer in meshRenderers) renderer.enabled = isVisible;

            image.enabled = isVisible;
            minimapIcon.SetActive(isVisible);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void StopCheckVisibility()
    {
        StopCoroutine(checkVisibilityCoroutine);
    }
}