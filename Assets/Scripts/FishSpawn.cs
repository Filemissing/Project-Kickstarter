using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public string fish;
    public FishSpawnSpawner fishSpawnSpawner;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("MINIGAMING");
            // Play minigame with fish values
        }
    }
}
