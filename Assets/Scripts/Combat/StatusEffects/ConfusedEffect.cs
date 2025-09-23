using UnityEngine;

public class ConfusedEffect : StatusEffect
{
    public ConfusedEffect(int level) 
    {
        this.level = level;
        icon = Resources.Load<Sprite>("Status Effect Icons/Confused");
    }

   // Sprite icon => Resources.Load<Sprite>("Status Effect Icons/Confused");

    public float missChance => 0.1f * level; // 10% to miss per level

    public override void ExecuteEffect(Combatant target)
    {
        // implemented in Attack Action
    }
}
