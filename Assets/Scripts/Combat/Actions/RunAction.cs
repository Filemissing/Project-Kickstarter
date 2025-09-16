using UnityEngine;

[CreateAssetMenu(fileName = "Run", menuName = "Combat/Actions/RunAction")]
public class RunAction : Action
{
    [SerializeField] int successRate = 50; // percentage chance to successfully flee
    public override void Execute(Combatant user, Combatant target)
    {
        base.Execute(user, target);
        if (user is PlayerCombat playerCombat)
        {
            bool success = Random.Range(0, 100) <= successRate;
            
            if(success)
            {
                CombatManager.instance.EndCombat(CombatEndState.Fled);
            }
            else
            {
                Debug.Log("Failed to flee!");
                user.EndTurn();
            }
        }
    }
}
