using UnityEngine;

[CreateAssetMenu(fileName = "Bleed Effect", menuName = "Combat/Status Effects/Bleed Effect")]
public class BleedEffect : StatusEffect
{
    public int damagePerLevel;
    public override void ExecuteEffect(Combatant target)
    {
        target.Damage(damagePerLevel * level, nonLethal: true);
        level--;
    }
}
