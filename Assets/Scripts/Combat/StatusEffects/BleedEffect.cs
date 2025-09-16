using UnityEngine;

[CreateAssetMenu(fileName = "Bleed Effect", menuName = "Combat/Status Effects/Bleed Effect")]
public class BleedEffect : StatusEffect
{
    public BleedEffect(int level) => this.level = level;

    int damage => 1 * level;
    public override void ExecuteEffect(Combatant target)
    {
        target.Damage(damage);
    }
}
