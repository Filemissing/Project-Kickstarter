using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            throw new System.Exception("Multiple instances of CombatManager detected!");
    }

    [SerializeField] GameObject enemyPos;

    public PlayerCombat playerCombat;
    public Enemy enemy;

    public combatState currentCombatState = combatState.playerTurn;
    public enum combatState
    {
        playerTurn,
        enemyTurn
    }

    public Enemy testEnemy;

    private void Start()
    {
        StartCombat(testEnemy);
    }

    public void NextTurn()
    {
        switch (currentCombatState)
        {
            case combatState.playerTurn:
                currentCombatState = combatState.enemyTurn;
                enemy.ExecuteMove();
                break;
            case combatState.enemyTurn:
                currentCombatState = combatState.playerTurn;
                playerCombat.StartTurn(new CombatState());
                break;
        }
    }

    public void StartCombat(Enemy enemy)
    {
        currentCombatState = combatState.playerTurn;
        playerCombat.Initialize();
        this.enemy = Instantiate(enemy, enemyPos.transform);
        playerCombat.StartTurn(new CombatState());
    }
    public void EndCombat(bool victory)
    {
        if (victory)
        {
            Debug.Log("Player won the combat!");
        }
        else
        {
            Debug.Log("Player lost the combat!");
        }
    }
}

public class CombatState
{
    public Attack playerAttack;
    public int playerAttackDamage;
    public Attack enemyAttack;
    public int enemyAttackDamage;

    public bool usedItems = false;
    public List<Item> usedItemsList = new();
}

public enum StatusEffectType
{
    None,
    Bleed,
    Poison,
    Entangled,
    Confused,
    Wet,
    SkillIssued
}
