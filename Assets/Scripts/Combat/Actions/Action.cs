using UnityEngine;

public class Action : ScriptableObject
{
    new public string name;
    public int oxygenCost;

    public virtual void Execute(Combatant user, Combatant target)
    {
        if (user is PlayerCombat playerCombat)
        {
            playerCombat.UseOxygen(oxygenCost);
        }
    }
}
