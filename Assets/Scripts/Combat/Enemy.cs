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
            int index = Random.Range(0, attacks.Count);
            selectedAttack = attacks[index];
        }

        Debug.Log(name + " is using the move: " + selectedAttack.name);

        selectedAttack.Execute(this, CombatManager.instance.playerCombat);
    }
}
