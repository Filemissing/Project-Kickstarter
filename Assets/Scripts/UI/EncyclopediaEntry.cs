using UnityEngine;

public class EncyclopediaEntry : MonoBehaviour
{
    public EnemyInfo enemyInfo;
    public Encyclopedia encyclopedia;
    
    public void OnClick()
    {
        encyclopedia.UpdateFishPanel(enemyInfo);
    }
}
