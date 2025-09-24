using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public EnemyInfo enemyInfo;
    public FishSpawnSpawner fishSpawnSpawner;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            BoatingManager.instance.canPlayerMoveList.Add(this);
            BoatingManager.instance.fishingMinigame.StartMinigame(enemyInfo.zonesAmount, enemyInfo);
            Destroy(gameObject);
        }
    }
}
