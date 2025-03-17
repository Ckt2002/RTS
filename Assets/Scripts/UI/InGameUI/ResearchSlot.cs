using System.Collections;
using System.Linq;
using GameSystem;
using UnityEngine;
using UnityEngine.UI;

public class ResearchSlot : ObjectSlot
{
    [SerializeField] private Image loading;

    private Coroutine researchCoroutine;
    private bool isSaved;
    public bool unlocked { get; set; }

    public override void BuyObject()
    {
        if (resourcesManager.Money < stat.Money)
            return;
        SetResearchCoroutine();
    }

    public void SetResearchCoroutine(float elapsedTime = 0f)
    {
        researchCoroutine ??= CoroutineManager.Instance?.StartManagedCoroutine(ResearchCoroutine(elapsedTime));
    }

    public void SetCompleted()
    {
        var matchingUnit =
            UnitManager.Instance.PlayerUnitsPrefab.FirstOrDefault(unit => unit.unitPrefab.name == stat.name);
        if (matchingUnit != null)
            matchingUnit.unlocked = true;
        loading.fillAmount = 0f;
        researchCoroutine = null;
        unlocked = true;
        SaveResearchSystem.SaveResearchPref(stat.GetComponent<UnitInfor>().name, 0, true);
        UnableSlot();
    }

    private IEnumerator ResearchCoroutine(float elapsedTime)
    {
        var buyTime = stat.GetComponent<UnitInfor>().ResearchTime;

        while (elapsedTime < buyTime)
        {
            if (PauseSystem.isPausing)
            {
                if (!isSaved)
                {
                    SaveResearchSystem.SaveResearchPref(stat.GetComponent<UnitInfor>().name, elapsedTime, false);
                    isSaved = true;
                }

                yield return null;
                continue;
            }

            elapsedTime += Time.deltaTime;
            loading.fillAmount = elapsedTime / buyTime;
            yield return null;
        }

        SetCompleted();
        yield return null;
    }
}