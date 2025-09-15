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
        target.Damage(damage);

        if(givesStatusEffect && statusEffect != null)
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

        base.Execute(user, target);

        user.EndTurn();
    }
}
