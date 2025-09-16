using UnityEngine;

[CreateAssetMenu(fileName = "Poison Effect", menuName = "Combat/Status Effects/PoisonEffect")]
public class PoisonEffect : StatusEffect
{
    public PoisonEffect(int level) => this.level = level;

    int damage = 5;
    public override void ExecuteEffect(Combatant target)
    {
        target.Damage(5, nonLethal: true);
    }
}
