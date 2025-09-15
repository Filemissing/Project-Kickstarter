using UnityEngine;

public class WetEffect : StatusEffect
{
    public WetEffect(int level) => this.level = level;

    float damageIncrease => 0.1f * level; // 10% more damage per level

    public override void ExecuteEffect(CombatState state, Combatant target)
    {
        // increase the damage taken (dealt by the enemy) by 10% per level of wetness
        if (target is PlayerCombat playerCombat)
        {
            state.enemyAttackDamage = Mathf.FloorToInt(state.enemyAttackDamage * (1 + damageIncrease));
        }
        else
        {
            state.playerAttackDamage = Mathf.FloorToInt(state.playerAttackDamage * (1 + damageIncrease));
        }
    }
}
