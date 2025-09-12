using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    [HideInInspector] public int level;
    public Sprite icon;

    public abstract void ExecuteEffect(Combatant target);
}
