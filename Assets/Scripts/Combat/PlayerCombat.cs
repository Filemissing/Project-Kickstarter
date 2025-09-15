using System.Linq;
using UnityEngine;

public class PlayerCombat : Combatant
{
    public PlayerStats playerStats;
    public CombatState combatState;

    public void Initialize()
    {
        playerStats.currentHealth = playerStats.maxHealth;
        playerStats.currentOxygen = playerStats.maxOxygen;

        maxHP = playerStats.maxHealth;
        currentHP = playerStats.currentHealth;
    }

    public void StartTurn(CombatState combatState)
    {
        this.combatState = combatState;
        CombatUIManager.instance.ShowActionMenu();
    }
    public override void EndTurn()
    {
        CombatUIManager.instance.HideAllMenus();
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
