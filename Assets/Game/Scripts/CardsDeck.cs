using UnityEngine;
using UnityEngine.EventSystems;

public class CardsDeck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardsManager cardsManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        cardsManager.HoveringMenu = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardsManager.HoveringMenu = null;
    }
}