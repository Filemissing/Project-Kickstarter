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

    public combatState currentCombatState = combatState.idle;
    public enum combatState
    {
        idle,
        playerTurn,
        enemyTurn,
    }

    public void StartCombat(Enemy enemy)
    {
        currentCombatState = combatState.playerTurn;
        enemy = Instantiate(enemy, enemyPos.transform.position, Quaternion.identity);
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
