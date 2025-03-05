using System.Collections;
using GameSystem;
using UnityEngine;
using UnityEngine.UI;

public class CapturePoint : MonoBehaviour
{
    [SerializeField] private Image captureCircle;

    [SerializeField] private float captureTime = 1f;

    //[SerializeField] private float timeToGetMoney = 1f;
    [SerializeField] private float radius = 1f;
    [SerializeField] private int moneyPerSeccond = 5;

    private bool captured;
    private float elapsedTime; // Thời gian đã trôi qua
    private ResourcesManager resourcesManager;

    private void Start()
    {
        resourcesManager = ResourcesManager.Instance;
        captureCircle.fillAmount = 0f;
        elapsedTime = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains(Names.Player))
        {
            StopAllCoroutines();
            StartCoroutine(StartCapturing());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Leave while capturing
        if (other.gameObject.name.Contains(Names.Player) && !captured)
        {
            StopAllCoroutines();
            StartCoroutine(StopCapturing());
        }
    }

    private IEnumerator StopCapturing()
    {
        if (elapsedTime == 0 && captureCircle.fillAmount == 0)
            yield break;
        while (elapsedTime > 0)
        {
            if (PauseSystem.isPausing)
            {
                yield return null;
                continue;
            }

            elapsedTime -= Time.deltaTime;
            captureCircle.fillAmount = elapsedTime / captureTime;
            yield return null;
        }
    }

    private IEnumerator StartCapturing()
    {
        if (captured)
            yield break;

        while (elapsedTime < captureTime)
        {
            if (PauseSystem.isPausing)
            {
                yield return null;
                continue;
            }

            elapsedTime += Time.deltaTime;
            captureCircle.fillAmount = elapsedTime / captureTime;
            yield return null;
        }

        captureCircle.fillAmount = 1f;
        captured = true;
        StartCoroutine(nameof(StartGetMoney));
    }

    private IEnumerator StartGetMoney()
    {
        while (captured)
        {
            if (PauseSystem.isPausing)
            {
                yield return null;
                continue;
            }

            resourcesManager.CurrentMoney(moneyPerSeccond);
            yield return new WaitForSeconds(1f);
        }
    }
}