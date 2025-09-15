using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Scriptable Objects/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    [Header("Basic Info")]
    public string name;
    public Sprite picture150px;
    
    [Header("Encyclopedia")]
    public Sprite picture500px;
    public Sprite picture100px;
    public string scientificName;
    public EndangermentChart.Endangerment endangerment;
    public Sprite naturalHabitat;
    public string description;
}
