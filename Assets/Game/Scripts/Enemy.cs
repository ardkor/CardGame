using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<EnemySlot> slots;
    public int attack;
    private GameManager gameManager;
    public TMP_Text attackText;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void TryDie()
    {
        foreach (var slot in slots)
        {
            if (!slot.isDead)
                return;
        }
        
        foreach (var slot in slots)
        {
            slot.Clear();
        }
        FindFirstObjectByType<GameManager>().WinFight();
    }
}