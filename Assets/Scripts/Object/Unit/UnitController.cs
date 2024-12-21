using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private UnitInfor unitStat;
    [SerializeField] private List<Renderer> renderers;
    private readonly float timeToHideAfterDied = 2f;
    private float deathTimer;

    private UnitManager unitManager;

    public bool IsDead { get; private set; }

    private void Start()
    {
        unitManager = UnitManager.Instance;
    }

    private void Update()
    {
        if (IsDead)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= timeToHideAfterDied) ResetStatus();

            return;
        }

        if (IsAlive())
            return;

        IsDead = true;
        unitManager.UnitsSelected.Remove(this);

        if (CompareTag(Tags.PlayerUnit))
            transform.GetComponent<PlayerRing>().UnitDeselected();

        foreach (var unitRenderer in renderers)
        {
            var originalColor = unitRenderer.material.color;
            var darkerColor = originalColor * 0.5f;
            unitRenderer.material.color = darkerColor;
        }
    }

    public bool IsAlive()
    {
        return unitStat.CurrentHealth > 0;
    }

    private void ResetStatus()
    {
        deathTimer = 0f;
        unitStat.ResetStat();

        foreach (var unitRenderer in renderers)
        {
            var originalColor = unitRenderer.material.color;
            var resetColor = originalColor / 0.5f;
            unitRenderer.material.color = resetColor;
        }

        IsDead = false;
        gameObject.SetActive(false);
    }
}