using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Combat/Weapon")]
public class Weapon : ScriptableObject
{
    new public string name;
    public Sprite icon;
    public List<Attack> attacks;
}
