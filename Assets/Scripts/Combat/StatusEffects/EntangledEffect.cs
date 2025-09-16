using UnityEngine;

public class EntangledEffect : StatusEffect
{
    public EntangledEffect(int level) => this.level = level;

    public override void ExecuteEffect(Combatant target)
    {
        // skips the target's next turn, implemented in the Combatant class
    }
}
