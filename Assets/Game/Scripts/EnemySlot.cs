using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Element element;
    [SerializeField] private int initPower;
    private Image image;
    private int currentPower;
    private ElementsManager elementsManager;
    [SerializeField] private Enemy enemy;
    [HideInInspector] public bool isDead = false;


    private CardsManager cardsManager;

    private void Awake()
    {
        cardsManager = FindFirstObjectByType<CardsManager>();
        elementsManager = FindFirstObjectByType<ElementsManager>();
        image = GetComponentsInChildren<Image>()[1];
        currentPower = initPower;
    }

    public void Clear()
    {
        List<Card> cards = new List<Card>(GetComponentsInChildren<Card>());
        foreach (Card card in cards)
        {
            Destroy(card.face.gameObject);
        }
        
        Destroy(gameObject);
    }
    public void GetAffected(Element attackElement, int attackPower)
    {
        if (element == attackElement)
            return;
        if (element == Element.Fire)
        {
            if (attackElement == Element.Ice)
                AffectPower(attackPower, 2f);
            else if (attackElement == Element.Life)
                AffectPower(attackPower, 1f);
            else if (attackElement == Element.Death)
                AffectPower(attackPower, 0.5f);
        }

        if (element == Element.Ice)
        {
            if (attackElement == Element.Life)
                AffectPower(attackPower, 2f);
            else if (attackElement == Element.Death)
                AffectPower(attackPower, 1f);
            else if (attackElement == Element.Fire)
                AffectPower(attackPower, 0.5f);
        }

        if (element == Element.Death)
        {
            if (attackElement == Element.Fire)
                AffectPower(attackPower, 2f);
            else if (attackElement == Element.Ice)
                AffectPower(attackPower, 1f);
            else if (attackElement == Element.Life)
                AffectPower(attackPower, 0.5f);
        }

        if (element == Element.Life)
        {
            if (attackElement == Element.Death)
                AffectPower(attackPower, 2f);
            else if (attackElement == Element.Fire)
                AffectPower(attackPower, 1f);
            else if (attackElement == Element.Ice)
                AffectPower(attackPower, 0.5f);
        }
    }

    private void AffectPower(int attackPower, float factor)
    {
        currentPower -= (int)(attackPower * factor);
        if (currentPower <= 0)
        {
            image.sprite = elementsManager.GetSlotSprite(element, 0);
            isDead = true;
            enemy.TryDie();
        }
        else
            image.sprite = elementsManager.GetSlotSprite(element, currentPower);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        cardsManager.HoveringMenu = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardsManager.HoveringMenu = null;
    }
}