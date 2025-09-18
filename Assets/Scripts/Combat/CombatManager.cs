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

    [Header("Static references")]
    [SerializeField] GameObject enemyPos;
    public PlayerCombat playerCombat;
    public RectTransform enemyHealthBar;
    [HideInInspector] public Enemy enemy;

    [Header("Combat State")]
    public combatState currentCombatState = combatState.playerTurn;

    [Header("Testing")]
    public Enemy testEnemy;
    private void Start()
    {
        StartCombat(testEnemy);
    }

    public void StartCombat(Enemy enemy)
    {
        currentCombatState = combatState.playerTurn;
        this.enemy = Instantiate(enemy, enemyPos.transform);
        this.enemy.healthBar = enemyHealthBar;
        playerCombat.StartTurn();
    }
    public void EndCombat(CombatEndState endState)
    {
        switch (endState)
        {
            case CombatEndState.Victory:
                Debug.Log("You won the fight!");
                break;
            case CombatEndState.Defeat:
                Debug.Log("You lost the fight...");
                break;
            case CombatEndState.Fled:
                Debug.Log("You fled the fight.");
                break;
        }
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
                playerCombat.StartTurn();
                break;
        }
    }

}

public enum combatState
{
    playerTurn,
    enemyTurn
}

public enum CombatEndState
{
    Victory,
    Defeat,
    Fled
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
