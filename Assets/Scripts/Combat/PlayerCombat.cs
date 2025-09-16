using System.Linq;
using UnityEngine;

public class PlayerCombat : Combatant
{
    public PlayerStats playerStats;

    [SerializeField] BreatheAction breatheAction;
    [SerializeField] RunAction runAction;

    [SerializeField] int maxOxygen;
    [SerializeField] int currentOxygen;

    public void Start()
    {
        // start combat with max health and oxygen
        maxHP = playerStats.maxHealth;
        currentHP = maxHP;

        maxOxygen = playerStats.maxOxygen;
        currentOxygen = maxOxygen;
    }

    public override void StartTurn()
    {
        base.StartTurn();
        CombatUIManager.instance.ShowActionMenu();
    }
    public override void EndTurn()
    {
        CombatUIManager.instance.HideAllMenus();
        base.EndTurn();
    }

    // additional actions specific to the player
    public void Breathe()
    {
        breatheAction.Execute(this, CombatManager.instance.enemy);
    }
    public void Run()
    {
        runAction.Execute(this, CombatManager.instance.enemy);
    }

    // helper funcitons for oxygen management
    public void UseOxygen(int amount)
    {
        currentOxygen -= amount;
        if (currentOxygen <= 0)
        {
            int deficit = Mathf.Abs(currentOxygen);
            currentOxygen = 0;

            Damage(deficit);
        }
    }
    public void RestoreOxygen(int amount)
    {
        currentOxygen += amount;
        if (currentOxygen > maxOxygen)
        {
            currentOxygen = maxOxygen;
        }
    }

    public override void Die()
    {
        CombatManager.instance.EndCombat(CombatEndState.Defeat);
    }
}
