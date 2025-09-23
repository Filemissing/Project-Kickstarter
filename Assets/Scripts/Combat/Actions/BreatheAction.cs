using UnityEngine;

[CreateAssetMenu(fileName = "Breathe", menuName = "Combat/Actions/BreatheAction")]
public class BreatheAction : Action
{
    [SerializeField] int oxygenRestored = 5;

    public override void Execute(Combatant user, Combatant target)
    {
        if (user is PlayerCombat playerCombat)
        {
            playerCombat.RestoreOxygen(oxygenRestored);
        }

        user.EndTurn();
    }
}
