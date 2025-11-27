using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public GameObject SelectedCard;
    [HideInInspector] public GameObject HoveringMenu;

    public CardsLayoutGroup cardsLayoutGroup;

    public Transform Deck;
    public GameObject CardParent;
    public List<GameObject> CardsFaces = new List<GameObject>();
    private List<int> spawnedCards = new List<int>();

    private void Start()
    {
        SpawnHandCards();
        SpawnDeck();
    }

    public void SpawnHandCards()
    {
        while(cardsLayoutGroup.transform.childCount < 4)
        {
            int randomCard = Random.Range(0, CardsFaces.Count);
            while (spawnedCards.Contains(randomCard))
            {
                randomCard = Random.Range(0, CardsFaces.Count);
                if (spawnedCards.Count == CardsFaces.Count)
                {
                    return;
                }
            }

            GameObject card = Instantiate(CardParent, cardsLayoutGroup.transform);
            
            spawnedCards.Add(randomCard);
            cardsLayoutGroup.Cards.Add(card.transform.GetChild(0).gameObject);
            GameObject cardFace = Instantiate(CardsFaces[randomCard], GameObject.Find("CardVisuals").transform);
            card.GetComponentInChildren<Card>().face = cardFace.GetComponent<CardFace>();
            cardFace.GetComponent<CardFace>().target = card.GetComponentInChildren<Card>().gameObject;
        }
    }

    public void SpawnDeck()
    {
        while(spawnedCards.Count < CardsFaces.Count)
        {
            int randomCard = Random.Range(0, CardsFaces.Count);
            while (spawnedCards.Contains(randomCard))
            {
                randomCard = Random.Range(0, CardsFaces.Count);
                if (spawnedCards.Count == CardsFaces.Count)
                {
                    return;
                }
            }
            
            GameObject card = Instantiate(CardParent, Deck);
            
            spawnedCards.Add(randomCard);
            GameObject cardFace = Instantiate(CardsFaces[randomCard], GameObject.Find("CardVisuals").transform);
            card.GetComponentInChildren<Card>().face = cardFace.GetComponent<CardFace>();
            cardFace.GetComponent<CardFace>().target = card.GetComponentInChildren<Card>().gameObject;
            
            card.GetComponentInChildren<Card>().isInDeck = true;
            cardFace.GetComponent<CardFace>().EnableBack();
        }
    }
}
