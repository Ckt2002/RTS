using System.Collections;
using GameSystem;
using UnityEngine;
using UnityEngine.UI;

public class CapturePoint : MonoBehaviour
{
    [SerializeField] private Image captureCircle;
    [SerializeField] private float captureTime = 1f;
    [SerializeField] private float radius = 1f;
    [SerializeField] private int moneyPerSecond = 5;

    private bool captured;
    public float elapsedTime { get; set; }
    private ResourcesManager resourcesManager;

    private void Start()
    {
        CapturePointManager.Instance.AddNewCapturePoint(this);
        resourcesManager = ResourcesManager.Instance;
        captureCircle.fillAmount = 0f;
        elapsedTime = 0;
        GetComponent<CapturePointVisibility>().enabled = true;
        GetComponent<FogOfWarUnit>().enabled = false;
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
        GetComponent<CapturePointVisibility>().StopCheckVisibility();
        GetComponent<CapturePointVisibility>().enabled = false;
        GetComponent<FogOfWarUnit>().enabled = true;
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

            resourcesManager.CurrentMoney(moneyPerSecond);
            yield return new WaitForSeconds(1f);
        }
    }
}