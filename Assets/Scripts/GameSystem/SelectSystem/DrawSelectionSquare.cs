using UnityEngine;

public class DrawSelectionSquare : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;

    public Vector2 startPos { get; set; }
    public Vector2 endPos { get; set; }

    public void ShowSquare()
    {
        selectionBox.gameObject.SetActive(true);
    }

    public void HideSquare()
    {
        selectionBox.gameObject.SetActive(false);
    }

    public void ResetBoxSize()
    {
        selectionBox.sizeDelta = Vector2.zero;
    }

    public void UpdateSelectionBox()
    {
        if (startPos == endPos)
            return;

        Vector2 boxStart = startPos;
        Vector2 boxSize = endPos - startPos;

        if (boxSize.x < 0)
        {
            boxStart.x += boxSize.x;
            boxSize.x = -boxSize.x;
        }
        if (boxSize.y < 0)
        {
            boxStart.y += boxSize.y;
            boxSize.y = -boxSize.y;
        }

        selectionBox.anchoredPosition = boxStart;
        selectionBox.sizeDelta = boxSize;
    }
}
