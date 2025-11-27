using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //[HideInInspector] public bool IsDragging;
    public bool CanDrag;
    public bool Played;
    Canvas canvas;
    [HideInInspector] public CardsManager cardsManager;
    [HideInInspector] public CardsDiscard cardsDiscard;
    [HideInInspector] public CardsLayoutGroup cardsLayoutGroup;
    [HideInInspector] public CardFace face;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private bool isDiscardCard = false;
    public bool isInDeck = false;
    [SerializeField] private bool disabledBack = false;

    private void Start()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();
        canvas = FindFirstObjectByType<Canvas>();
        cardsManager = FindFirstObjectByType<CardsManager>();
        cardsDiscard = FindFirstObjectByType<CardsDiscard>();
        cardsLayoutGroup = FindFirstObjectByType<CardsLayoutGroup>();
        CanDrag = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CanDrag)
        {
            canvasGroup.blocksRaycasts = false;
            if (cardsDiscard.currentCard == this)
                isDiscardCard = true;
            else
                isDiscardCard = false;
            if (!isDiscardCard && !isInDeck)
                cardsManager.SelectedCard = gameObject;

            disabledBack = face.TryDisableBack();
            //Set booleans
            //IsDragging = true;

            cardsManager.GetComponent<AudioSource>().Play();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CanDrag)
        {
            //Dragging the object
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform,
                Input.mousePosition, canvas.worldCamera, out position);
            transform.position = canvas.transform.TransformPoint(position);
            /*if (cardsManager.HoveringMenu != null)
                Debug.Log(cardsManager.HoveringMenu.name);*/
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!CanDrag)
            return;
        if (cardsManager.HoveringMenu != null &&
            cardsManager.HoveringMenu.GetComponent<CardsDiscard>())
        {
            if (!isDiscardCard && !isInDeck)
            {
                transform.parent.position = cardsManager.HoveringMenu.transform.position;
                transform.parent.SetParent(cardsManager.HoveringMenu.transform);

                transform.position = transform.parent.position;

                cardsManager.cardsLayoutGroup.Cards.Remove(gameObject);
                cardsManager.HoveringMenu.GetComponent<CardsDiscard>().ReplaceCard(this);
            }
            else
            {
                transform.position = transform.parent.position;
                if (disabledBack)
                    face.EnableBack();
            }
        }
        else if (cardsManager.HoveringMenu != null && cardsManager.HoveringMenu.GetComponent<EnemySlot>()
                                                   && !cardsManager.HoveringMenu.GetComponent<EnemySlot>().isDead)
        {
            CanDrag = false;
            Played = true;
            transform.parent.position = cardsManager.HoveringMenu.transform.position;
            transform.parent.SetParent(cardsManager.HoveringMenu.transform);
            transform.parent.SetSiblingIndex(cardsManager.HoveringMenu.transform.childCount);

            transform.position = transform.parent.position;

            cardsManager.cardsLayoutGroup.Cards.Remove(gameObject);

            cardsManager.HoveringMenu.GetComponent<EnemySlot>().GetAffected(face.element, face.power);
            if (isDiscardCard)
                cardsDiscard.SetCardNull();
            if (isInDeck)
                isInDeck = false;
        }
        else if (cardsManager.HoveringMenu != null && cardsManager.HoveringMenu.GetComponent<CardsLayoutGroup>() &&
                 isInDeck && !isDiscardCard && cardsLayoutGroup.CardCount < 4) // InDeck to CardsLayoutGroup
        {
            transform.position = transform.parent.position;
            transform.parent.SetParent(cardsLayoutGroup.transform);
            transform.parent.position = cardsLayoutGroup.transform.position;
            cardsManager.cardsLayoutGroup.Cards.Add(gameObject);
            isInDeck = false;
        }
        else
        {
            if (isDiscardCard && cardsLayoutGroup.CardCount < 4) // discard to null or CardsLayoutGroup
            {
                cardsDiscard.SetCardNull();
                transform.position = transform.parent.position;
                transform.parent.SetParent(cardsLayoutGroup.transform);
                transform.parent.position = cardsLayoutGroup.transform.position;
                cardsManager.cardsLayoutGroup.Cards.Add(gameObject);
            }
            else
            {
                transform.position = transform.parent.position;
                if (disabledBack)
                    face.EnableBack();
            }
        }

        cardsManager.SelectedCard = null;
        cardsManager.GetComponent<AudioSource>().Play();
        canvasGroup.blocksRaycasts = true;
        //Set booleans
        //IsDragging = true;
    }
}