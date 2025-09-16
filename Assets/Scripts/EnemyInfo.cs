using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Scriptable Objects/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    [Header("Basic Info")]
    public string name;
    public Sprite sprite150px;
    
    [Header("Encyclopedia")]
    public Sprite sprite500px;
    public Sprite sprite100px;
    public string scientificName;
    public EndangermentChart.Endangerment endangerment;
    public Sprite naturalHabitat;
    public string description;
}
