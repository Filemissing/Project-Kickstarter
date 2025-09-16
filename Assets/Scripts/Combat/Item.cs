using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Combat/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public string description;
    public Sprite icon;

    [Tooltip("if false, will remove effect instead of adding")]
    public bool givesStatusEffect;
    public StatusEffectType statusEffectType;
    StatusEffect statusEffect;

    private void OnEnable()
    {
        statusEffect = statusEffectType switch
        {
            StatusEffectType.Bleed => new BleedEffect(1),
            StatusEffectType.Poison => new PoisonEffect(1),
            StatusEffectType.Entangled => new EntangledEffect(1),
            StatusEffectType.Confused => new ConfusedEffect(1),
            StatusEffectType.Wet => new WetEffect(1),
            StatusEffectType.SkillIssued => new SkillIssuedEffect(1),
            _ => null,
        };
    }

    public void Use(Combatant user, Combatant target)
    {
        if(givesStatusEffect)
        {
            statusEffect.ApplyEffect(target);
        }
        else
        {
            statusEffect.RemoveEffect(target);
        }

        CombatManager.instance.playerCombat.playerStats.items.Remove(this);

        CombatUIManager.instance.ConstructItemsMenu();
    }
}
