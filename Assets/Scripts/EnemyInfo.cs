using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Scriptable Objects/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    [Header("Basic Info")]
    new public string name;
    public string catchPhrase;
    public Sprite sprite150px;

    [Header("Combat")]
    public Enemy enemy;

    [Header("Fish Spawn")]
    [Tooltip("Between 1-10")]
    [Range(1, 10)]
    public float rarity = 1f;
    
    [Header("Fishing Minigame")]
    public int zonesAmount = 3;
    
    [Header("Encyclopedia")]
    public Sprite sprite500px;
    public Sprite sprite100px;
    public string scientificName;
    public EndangermentChart.Endangerment endangerment;
    public Sprite naturalHabitat;
    public string description;
}
