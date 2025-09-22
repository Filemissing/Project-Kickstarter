using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Combat/Actions/Attack")]
public class Attack : Action
{
    public int damage;

    public bool givesStatusEffect;
    public StatusEffectType statusEffectType;
    StatusEffect statusEffect;
    public int effectLevel;

    public bool hasFollowUp;
    public bool isForced;
    public Attack followUpAttack;

    [Header("Enemy specific")]
    public int chance;
    public int oxygenDrain;

    public void OnEnable()
    {
        statusEffect = statusEffectType switch
        {
            StatusEffectType.Bleed => new BleedEffect(effectLevel),
            StatusEffectType.Poison => new PoisonEffect(effectLevel),
            StatusEffectType.Entangled => new EntangledEffect(effectLevel),
            StatusEffectType.Confused => new ConfusedEffect(effectLevel),
            StatusEffectType.Wet => new WetEffect(effectLevel),
            StatusEffectType.SkillIssued => new SkillIssuedEffect(effectLevel),
            _ => null,
        };
    }

    public override void Execute(Combatant user, Combatant target)
    {
        // determine if the attack hits or misses
        if (user.statusEffects.Find(e => e is ConfusedEffect) is ConfusedEffect confusedEffect)
        {
            if (Random.value < confusedEffect.missChance)
            {
                Debug.Log(user.name + "'s attack missed due to confusion!");
                user.EndTurn();
                return;
            }
        }

        // apply possible status effect to target
        if (givesStatusEffect && statusEffect != null)
        {
            if(target.statusEffects.Contains(statusEffect))
                target.statusEffects.Find(e => e == statusEffect).level += effectLevel;
            else
            {
                target.statusEffects.Add(statusEffect);
                target.statusEffects.Find(e => e == statusEffect).level = effectLevel;
            }
        }

        if (hasFollowUp && followUpAttack != null)
        {
            if(user is PlayerCombat playerCombat)
            {
                playerCombat.playerStats.currentWeapon.attacks.Remove(this);
                playerCombat.playerStats.currentWeapon.attacks.Add(followUpAttack);
            }
            else if (user is Enemy enemy)
            {
                enemy.attacks.Remove(this);
                enemy.attacks.Add(followUpAttack);
            }
        }

        // apply damage modifiers from status effects
        int finalDamage = damage;

        if (user.statusEffects.Find(e => e is SkillIssuedEffect) is SkillIssuedEffect skillIssuedEffect)
            finalDamage = Mathf.RoundToInt(damage * skillIssuedEffect.damageMultiplier);

        if (target.statusEffects.Find(e => e is WetEffect) is WetEffect wetEffect)
            finalDamage = Mathf.RoundToInt(finalDamage * wetEffect.damageMultiplier);

        target.Damage(finalDamage);

        if(target is PlayerCombat playerCombat2)
        {
            playerCombat2.UseOxygen(oxygenDrain);
        }

        //additional logic
        base.Execute(user, target);

        user.EndTurn();
    }
}
