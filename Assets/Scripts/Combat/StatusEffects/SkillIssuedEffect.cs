using UnityEngine;

public class SkillIssuedEffect : StatusEffect
{
    public SkillIssuedEffect(int level) => this.level = level;

    public override void ExecuteEffect(CombatState state, Combatant target)
    {
        throw new System.NotImplementedException();
    }
}
