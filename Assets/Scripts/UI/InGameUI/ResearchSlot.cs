using System.Collections;
using System.Linq;
using GameSystem;
using UnityEngine;
using UnityEngine.UI;

public class ResearchSlot : ObjectSlot
{
    [SerializeField] private Image loading;
    private Coroutine researchCoroutine;

    public override void BuyObject()
    {
        base.BuyObject();
        researchCoroutine ??= CoroutineManager.Instance?.StartManagedCoroutine(ResearchCoroutine());
    }

    private IEnumerator ResearchCoroutine()
    {
        var buyTime = stat.GetComponent<UnitInfor>().ResearchTime;
        var elapsedTime = 0f;

        while (elapsedTime < buyTime)
        {
            if (PauseSystem.isPausing)
            {
                Debug.Log("Coroutine Paused");
                yield return null;
                continue;
            }

            elapsedTime += Time.deltaTime;
            loading.fillAmount = elapsedTime / buyTime;
            yield return null;
        }

        var matchingUnit =
            UnitManager.Instance.PlayerUnitsPrefab.FirstOrDefault(unit => unit.unitPrefab.name == stat.name);
        if (matchingUnit != null)
            matchingUnit.unlocked = true;
        UnableSlot();
        loading.fillAmount = 0f;
        researchCoroutine = null;
    }
}