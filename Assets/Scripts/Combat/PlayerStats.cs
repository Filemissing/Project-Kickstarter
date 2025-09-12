using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int maxHealth;
    public int currentHealth;

    public int maxOxygen;
    public int currentOxygen;

    public Weapon currentWeapon;
    public List<Weapon> unlockedWeapons = new();

    public List<Item> items = new();
}
