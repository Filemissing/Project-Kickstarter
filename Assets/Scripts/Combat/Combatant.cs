using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;

public abstract class Combatant : MonoBehaviour
{
    new public string name;

    [Header("Health")]
    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;
    public RectTransform healthBar;

    public List<StatusEffect> statusEffects = new();

    public virtual void Start()
    {
        UpdateHPbar();
    }

    public virtual void StartTurn()
    {
        if (statusEffects.Any(se => se is EntangledEffect))
        {
            EndTurn();
        }
    }
    public virtual void EndTurn()
    {
        StatusEffect[] tempStatusEffects = statusEffects.ToArray();
        foreach (StatusEffect effect in tempStatusEffects)
        {
            effect.ExecuteEffect(this);
            effect.RemoveEffectLevel(this, 1);
        }
        CombatManager.instance.NextTurn();
    }

    public virtual void Damage(int damage, bool nonLethal = false)
    {
        currentHP -= damage;
        UpdateHPbar();
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
        UpdateHPbar();
    }
    public abstract void Die();

    void UpdateHPbar()
    {
        float newValue = (float)currentHP / (float)maxHP;
        healthBar.localScale = new Vector3(1f, Mathf.Clamp01(newValue), 1f);
    }

    // testing purposes only
    [Button] void ListStatusEffects()
    {
        foreach (StatusEffect effect in statusEffects)
        {
            Debug.Log(effect.GetType().Name + " Level: " + effect.level);
        }
    }
}
