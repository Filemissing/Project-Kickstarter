using UnityEngine;

public abstract class StatusEffect
{
    [HideInInspector] public int level;
    public Sprite icon;

    public void ApplyEffect(Combatant target)
    {
        if(target.statusEffects.Contains(this))
            target.statusEffects.Find(se => se == this).level += 1;
        else
            target.statusEffects.Add(this);

        target.UpdateStatusBar(this, true);
    }

    public void RemoveEffectLevel(Combatant target, int level)
    {
        if(target.statusEffects.Contains(this))
        {
            target.statusEffects.Find(se => se == this).level -= level;
            if(target.statusEffects.Find(se => se == this).level <= 0)
            {
                target.statusEffects.Remove(this);
                target.UpdateStatusBar(this, false);
            }
            else
            {
                target.UpdateStatusBar(this, true);
            }
        }
    }

    public void RemoveEffect(Combatant target)
    {
        if(target.statusEffects.Contains(this))
        {
            target.statusEffects.Remove(this);
            target.UpdateStatusBar(this, false);
        }
    }

    public abstract void ExecuteEffect(Combatant target);
}
