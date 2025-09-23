using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [Header("Enemy References")]
    public RectTransform enemyHealthBar;
    public RectTransform enemyStatusBar;
    public StatusEffectIcon statusIconPrefab;
    public RectTransform enemyMoveNameSpace;
    public MoveNameAnimator enemyMoveNamePrefab;

    [HideInInspector] public Enemy enemy;

    [Header("Combat State")]
    public combatState currentCombatState = combatState.playerTurn;

    public void StartCombat(EnemyInfo enemyInfo, bool wonMinigame)
    {
        currentCombatState = combatState.playerTurn;

        // assign enemy variables
        enemy = Instantiate(enemyInfo.enemy, enemyPos.transform);
        enemy.enemyInfo = enemyInfo;
        enemy.healthBar = enemyHealthBar;
        enemy.statusBar = enemyStatusBar;
        enemy.statusIconPrefab = statusIconPrefab;
        enemy.moveNameSpace = enemyMoveNameSpace;
        enemy.moveNamePrefab = enemyMoveNamePrefab;

        // apply miniGame results
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
                GameManager.instance.playerStats.currency += enemy.enemyInfo.currencyDropAmount;
                GameManager.instance.unlockedEnemyInfos.Add(enemy.enemyInfo);
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
