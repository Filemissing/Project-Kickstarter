using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : Combatant
{
    public List<Attack> attacks = new();

    public override void Die()
    {
        CombatManager.instance.EndCombat(CombatEndState.Victory);
        Destroy(gameObject);
    }

    public override void StartTurn()
    {
        base.StartTurn();
        ExecuteMove();
    }

    public void ExecuteMove()
    {
        Attack selectedAttack = attacks.FirstOrDefault(a => a.isForced);

        if(selectedAttack == null)
        {
            int rng = Random.Range(0, 101);
            foreach (Attack attack in attacks)
            {
                if(rng < attack.chance)
                {
                    selectedAttack = attack;
                    break;
                }
                else
                    rng -= attack.chance;
            }
        }

        Debug.Log(name + " is using the move: " + selectedAttack.name);

        selectedAttack.Execute(this, CombatManager.instance.playerCombat);
    }
}
