using UnityEngine;

public abstract class StatusEffect
{
    [HideInInspector] public int level;
    public Sprite icon;

    public void ApplyEffect(Combatant target)
    {
        StatusEffect existing = target.statusEffects.Find(se => se.icon == icon);

        if (existing != null)
        {
            existing.level += level;
            target.UpdateStatusBar(existing, true);
        }
        else
        {
            target.statusEffects.Add(this);
            target.UpdateStatusBar(this, true);
        }
    }

    public void RemoveEffectLevel(Combatant target, int level)
    {
        StatusEffect existing = target.statusEffects.Find(se => se.icon == icon);

        if (existing != null)
        {
            existing.level -= level;
            if(existing.level <= 0)
            {
                target.statusEffects.Remove(existing);
                target.UpdateStatusBar(existing, false);
            }
            else
            {
                target.UpdateStatusBar(existing, true);
            }
        }
    }

    public void RemoveEffect(Combatant target)
    {
        StatusEffect existing = target.statusEffects.Find(se => se.icon == icon);

        if (existing != null)
        {
            target.statusEffects.Remove(existing);
            target.UpdateStatusBar(existing, false);
        }
    }

    public abstract void ExecuteEffect(Combatant target);
}
