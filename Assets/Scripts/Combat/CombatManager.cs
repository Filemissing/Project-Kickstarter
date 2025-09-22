using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Combatant setup referenes")]
    [SerializeField] GameObject enemyPos;
    public PlayerCombat playerCombat;
    public RectTransform enemyHealthBar;
    [HideInInspector] public Enemy enemy;

    [Header("Combat State")]
    public combatState currentCombatState = combatState.playerTurn;

    [Header("Intro")]
    [SerializeField] float introDuration;

    public void StartCombat(EnemyInfo enemyInfo, bool wonMinigame)
    {
        currentCombatState = combatState.playerTurn;
        enemy = Instantiate(enemyInfo.enemy, enemyPos.transform);
        enemy.healthBar = enemyHealthBar;
        if (!wonMinigame)
        {
            playerCombat.statusEffects.Add(new SkillIssuedEffect(2));
            playerCombat.statusEffects.Add(new EntangledEffect(1));
        }

        CombatUIManager.instance.ShowIntro(enemyInfo);
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

        GameManager.instance.currentPlayerMode = playerMode.boating;
        SceneManager.LoadScene("Main");
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
