using UnityEngine;
using System.Collections.Generic;

public abstract class Combatant : MonoBehaviour
{
    new public string name;
    public int maxHP;
    public int currentHP;

    public List<StatusEffect> statusEffects = new();

    public virtual void EndTurn()
    {
        foreach (StatusEffect effect in statusEffects)
        {
            effect.ExecuteEffect(this);
            if (effect.level <= 0)
            {
                statusEffects.Remove(effect);
            }
        }
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
}
