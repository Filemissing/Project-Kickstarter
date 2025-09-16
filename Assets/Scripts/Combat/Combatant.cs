using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;

public abstract class Combatant : MonoBehaviour
{
    new public string name;
    public int maxHP;
    public int currentHP;

    public List<StatusEffect> statusEffects = new();

    public virtual void StartTurn()
    {
        if (statusEffects.Any(se => se is EntangledEffect))
        {
            EndTurn();
        }
    }
    public virtual void EndTurn()
    {
        foreach (StatusEffect effect in statusEffects)
        {
            effect.ExecuteEffect(this);
            effect.RemoveEffectLevel(this, 1);
        }
        CombatManager.instance.NextTurn();
    }

    public virtual void Damage(int damage, bool nonLethal = false)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            if (nonLethal)
                currentHP = 1;
            else
            {
                currentHP = 0;
                Die();
            }
        }
    }
    public virtual void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
    public abstract void Die();

    // testing purposes only
    [Button] void ListStatusEffects()
    {
        foreach (StatusEffect effect in statusEffects)
        {
            Debug.Log(effect.GetType().Name + " Level: " + effect.level);
        }
    }
}
