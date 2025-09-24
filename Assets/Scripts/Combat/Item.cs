using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Combat/Item")]
public class Item : ScriptableObject
{
    [Header("Info")]
    new public string name;
    public string description;
    public string details;
    public Sprite icon;
    public int cost;

    [Header("Combat Stats")]
    [Tooltip("if false, will remove effect instead of adding")]
    public bool givesStatusEffect;
    public int appliedLevel;
    public StatusEffectType statusEffectType;
    public int healing;

    public void Use(Combatant user, Combatant target)
    {
        if (statusEffectType != StatusEffectType.None)
        {
            StatusEffect statusEffect = statusEffectType switch
            {
                StatusEffectType.Bleed => new BleedEffect(1),
                StatusEffectType.Poison => new PoisonEffect(1),
                StatusEffectType.Entangled => new EntangledEffect(1),
                StatusEffectType.Confused => new ConfusedEffect(1),
                StatusEffectType.Wet => new WetEffect(1),
                StatusEffectType.SkillIssued => new SkillIssuedEffect(1),
                _ => null,
            };

            statusEffect.level = appliedLevel;

            if (givesStatusEffect)
            {
                statusEffect.ApplyEffect(target);
            }
            else
            {
                statusEffect.RemoveEffect(user);
            } 
        }

        if (healing != 0)
        {
            user.Heal(healing);
        }

        CombatManager.instance.playerCombat.playerStats.items.Remove(this);

        CombatUIManager.instance.ConstructItemsMenu();
    }
}
