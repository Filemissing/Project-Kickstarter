using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public EnemyInfo enemyInfo;
    public FishSpawnSpawner fishSpawnSpawner;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.canPlayerMove = false;
            GameManager.instance.fishingMinigame.StartMinigame(enemyInfo.zonesAmount, enemyInfo.enemy);
            Destroy(gameObject);
        }
    }
}
