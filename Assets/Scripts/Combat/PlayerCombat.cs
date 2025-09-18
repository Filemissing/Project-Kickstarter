using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerCombat : Combatant
{
    [Header("Oxygen")]
    [SerializeField] int maxOxygen;
    [SerializeField] int currentOxygen;
    [SerializeField] RectTransform oxygenBar;

    [Header("Player Stats")]
    public PlayerStats playerStats;

    [Header("Player Specific Actions")]
    [SerializeField] BreatheAction breatheAction;
    [SerializeField] RunAction runAction;

    public override void Start()
    {
        // start combat with max health and oxygen
        maxHP = playerStats.maxHealth;
        currentHP = maxHP;

        maxOxygen = playerStats.maxOxygen;
        currentOxygen = maxOxygen;
        UpdateOxygenBar();

        base.Start();
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
        UpdateOxygenBar();
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
        UpdateOxygenBar();
    }

    void UpdateOxygenBar()
    {
        float newValue = (float)currentOxygen / maxOxygen;
        oxygenBar.localScale = new Vector3(1f, Mathf.Clamp01(newValue), 1f);
    }

    public override void Die()
    {
        CombatManager.instance.EndCombat(CombatEndState.Defeat);
    }
}
