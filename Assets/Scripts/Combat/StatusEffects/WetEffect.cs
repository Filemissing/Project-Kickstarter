using UnityEngine;

public class WetEffect : StatusEffect
{
    public WetEffect(int level)
    {
        this.level = level;
        icon = Resources.Load<Sprite>("Status Effect Icons/Wet");
    }

    public float damageMultiplier => 1 + 0.1f * level; // 10% more damage per level

    public override void ExecuteEffect(Combatant target)
    {
        // implemented in Attack Action
    }
}
