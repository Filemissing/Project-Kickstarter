using System.Linq;
using UnityEngine;

public class PlayerCombat : Combatant
{
    public PlayerStats playerStats;

    private void Start()
    {
        maxHP = playerStats.maxHealth;
        currentHP = playerStats.currentHealth;
    }

    public override void EndTurn()
    {
        base.EndTurn();
    }

    public void UseOxygen(int amount)
    {
        playerStats.currentOxygen -= amount;
        if (playerStats.currentOxygen <= 0)
        {
            int deficit = Mathf.Abs(playerStats.currentOxygen);
            playerStats.currentOxygen = 0;

            Damage(deficit);
        }
    }
    public void RestoreOxygen(int amount)
    {
        playerStats.currentOxygen += amount;
        if (playerStats.currentOxygen > playerStats.maxOxygen)
        {
            playerStats.currentOxygen = playerStats.maxOxygen;
        }
    }

    public override void Damage(int damage, bool nonLethel = false)
    {
        base.Damage(damage, nonLethel);
        playerStats.currentHealth = currentHP;
    }
    public override void Heal(int healAmount)
    {
        base.Heal(healAmount);
        playerStats.currentHealth = currentHP;
    }
    public override void Die()
    {
        CombatManager.instance.EndCombat(false);
    }
}
