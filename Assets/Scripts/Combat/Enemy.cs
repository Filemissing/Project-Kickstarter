using System.Collections.Generic;
using UnityEngine;

public class Enemy : Combatant
{
    public List<Attack> attacks = new();

    public override void Die()
    {
        CombatManager.instance.EndCombat(true);
        Destroy(gameObject);
    }
}
