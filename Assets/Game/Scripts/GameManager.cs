using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private Transform enemiesParent;
    [SerializeField] private CardsDiscard discard;
    private int currentFight;
    [SerializeField] private List<Sprite> backgroundSprites;

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject endScreen;
    private Enemy currentEnemy;
    private int health;

    void Start()
    {
        health = 100;
        healthText.text = health.ToString();
        currentFight = 0;
        SpawnNextEnemy();
    }

    public void TryGetAttack()
    {
        health -= currentEnemy.attack;
        currentEnemy.attack += currentEnemy.attack;
        currentEnemy.attackText.text = currentEnemy.attack.ToString();
        healthText.text = health.ToString();
        if (health <= 0)
            endScreen.gameObject.SetActive(true);
    }

    private void SpawnNextEnemy()
    {
        if (enemies.Count > currentFight)
        {
            GameObject enemyObj = Instantiate(enemies[currentFight], enemiesParent);
            currentEnemy = enemyObj.GetComponentInChildren<Enemy>();
            background.sprite = backgroundSprites[currentFight];
            currentFight++;
        }
    }

    private bool isSpawning = false;

    public void EndTurn()
    {
        if (!isSpawning)
        {
            discard.PlaceCardInDeck();
            TryGetAttack();
        }
    }

    public void WinFight()
    {
        StartCoroutine(NewLevel());
    }

    IEnumerator NewLevel()
    {
        isSpawning = true;
        yield return new WaitForSeconds(1f);
        Destroy(currentEnemy.transform.parent.gameObject);
        SpawnNextEnemy();
        isSpawning = false;
        discard.PlaceCardInDeck();
    }
}