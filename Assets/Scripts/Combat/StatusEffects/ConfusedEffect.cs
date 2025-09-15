using UnityEngine;

public class ConfusedEffect : StatusEffect
{
    public ConfusedEffect(int level) => this.level = level;

    public float missChance => 0.2f * level; // 20% per level

    public override void ExecuteEffect(CombatState state, Combatant target)
    {
        // chance to miss
        throw new System.NotImplementedException();
    }
}
