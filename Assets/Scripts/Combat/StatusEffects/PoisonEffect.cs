using UnityEngine;

public class PoisonEffect : StatusEffect
{
    public PoisonEffect(int level)
    {
        this.level = level;
        icon = Resources.Load<Sprite>("Status Effect Icons/Poison");
    }

    int damage = 5;
    public override void ExecuteEffect(Combatant target)
    {
        target.Damage(5, nonLethal: true);
    }
}
