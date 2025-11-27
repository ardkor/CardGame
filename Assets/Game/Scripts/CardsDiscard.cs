using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardsDiscard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card currentCard;
    public CardsManager cardsManager;

    public void ReplaceCard(Card card)
    {
        RemoveCardToLayout();
        currentCard = card;
    }

    public void SetCardNull()
    {
        currentCard = null;
    }
    private void RemoveCardToLayout()
    {
        if (currentCard != null)
        {
            currentCard.transform.parent.position = cardsManager.cardsLayoutGroup.transform.position;
            currentCard.transform.parent.SetParent(cardsManager.cardsLayoutGroup.transform);
            currentCard.transform.position = currentCard.transform.parent.position;
            cardsManager.cardsLayoutGroup.Cards.Add(currentCard.gameObject);
        }
    }
    public void PlaceCardInDeck()
    {
        if (currentCard != null)
        {
            currentCard.transform.parent.position = cardsManager.Deck.transform.position;
            currentCard.transform.parent.SetParent(cardsManager.Deck.transform);
            currentCard.transform.position = currentCard.transform.parent.position;
            currentCard.transform.parent.SetSiblingIndex(0);
            currentCard.face.transform.SetSiblingIndex(0);
            currentCard.face.EnableBack();
            currentCard.isInDeck = true;
            currentCard = null;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        cardsManager.HoveringMenu = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("exit");
        cardsManager.HoveringMenu = null;
    }
}
