using UnityEngine;

public class ConfusedEffect : StatusEffect
{
    public ConfusedEffect(int level) => this.level = level;

    public float missChance => 0.1f * level; // 10% to miss per level

    public override void ExecuteEffect(Combatant target)
    {
        // implemented in Attack Action
    }
}
