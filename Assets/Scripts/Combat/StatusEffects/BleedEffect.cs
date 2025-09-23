using UnityEngine;

public class BleedEffect : StatusEffect
{
    public BleedEffect(int level)
    {
        this.level = level;
        icon = Resources.Load<Sprite>("Status Effect Icons/Bleed");
    }

    int damage => 1 * level;
    public override void ExecuteEffect(Combatant target)
    {
        target.Damage(damage);
    }
}
