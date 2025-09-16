using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int maxHealth;

    public int maxOxygen;

    public Weapon currentWeapon;
    public List<Weapon> unlockedWeapons = new();

    public List<Item> items = new();
}
