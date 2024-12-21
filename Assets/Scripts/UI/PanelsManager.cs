using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    [SerializeField] private GameObject buildingPanel;
    [SerializeField] private GameObject unitPanel;
    [SerializeField] private GameObject researchPanel;

    public void ShowBuildingPanel()
    {
        buildingPanel.SetActive(true);
        unitPanel.SetActive(false);
        researchPanel.SetActive(false);
    }

    public void ShowUnitPanel()
    {
        unitPanel.SetActive(true);
        buildingPanel.SetActive(false);
        researchPanel.SetActive(false);
    }

    public void ShowResearchPanel()
    {
        researchPanel.SetActive(true);
        buildingPanel.SetActive(false);
        unitPanel.SetActive(false);
    }
}