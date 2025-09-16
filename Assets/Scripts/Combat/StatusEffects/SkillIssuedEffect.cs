using UnityEngine;

public class SkillIssuedEffect : StatusEffect
{
    public SkillIssuedEffect(int level) => this.level = level;
    public float damageMultiplier => Mathf.Clamp01(1 - 0.1f * level); // 10% less damage per level, max 100% reduction

    public override void ExecuteEffect(Combatant target)
    {
        // implemented in Attack Action
    }
}
