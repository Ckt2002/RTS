using System.Collections;
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
        if (other.gameObject.name.Contains("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(StartCapturing());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Leave while capturing
        if (other.gameObject.name.Contains("Player") && !captured)
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
            elapsedTime -= Time.deltaTime; // Cập nhật thời gian
            captureCircle.fillAmount = elapsedTime / captureTime; // Cập nhật giá trị fillAmount
            yield return null; // Đợi frame tiếp theo
        }
    }

    private IEnumerator StartCapturing()
    {
        if (captured)
            yield break;

        while (elapsedTime < captureTime)
        {
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
        Debug.Log("Get in here");
        while (captured)
        {
            Debug.Log("Get money");
            resourcesManager.CurrentMoney(moneyPerSeccond);
            yield return new WaitForSeconds(1f);
        }
    }
}