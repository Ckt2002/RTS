using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ObjectSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected ObjectDescription description;
    [SerializeField] protected Image icon;
    [SerializeField] protected ResourcesManager resourcesManager;
    protected ObjectInfor stat;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!GetComponent<Button>().interactable)
            return;
        if (stat == null)
            return;

        description.gameObject.SetActive(true);
        description.SetDescription(icon.sprite, stat.Money, stat.Description);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        description.gameObject.SetActive(false);
    }

    public void SetSlotObject(GameObject objectToGet)
    {
        stat = objectToGet.GetComponent<ObjectInfor>();
        icon.sprite = stat.Icon;
        icon.color = new Color32(225, 225, 225, 225);
        GetComponent<Button>().interactable = true;
    }

    public void UnableSlot()
    {
        icon.color = new Color32(0, 0, 0, 150);
        GetComponent<Button>().interactable = false;
    }

    public virtual void BuyObject()
    {
        // if (resourcesManager.Money < stat.Money)
        //     return;
    }
}