using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishSpawnSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float minDistance = 5;
    [SerializeField] private Vector3 startPosition = Vector3.zero;
    public List<FishSpawn> fishSpawns = new List<FishSpawn>();
    
    [Header("Instances")]
    [SerializeField] private FishSpawn fishSpawnPrefab;


    
    bool SpawnFishSpawn()
    {
        int tries = 10;
        Vector3 currentSpawnPosition = Vector3.zero;
        bool isSuccessfull = false;
        
        Vector3 GetSpawnPosition()
        {
            float minX = transform.position.x - transform.localScale.x/2;
            float maxX = transform.position.x + transform.localScale.x/2;
            float minZ = transform.position.z - transform.localScale.z/2;
            float maxZ = transform.position.z + transform.localScale.z/2;
            
            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ)); 
            return spawnPosition;
        }

        void TryGetSpawnPosition()
        {
            Vector3 spawnPosition = GetSpawnPosition();
            tries--;
        
            bool invalidSpawn = false;
            foreach (FishSpawn fishSpawn in fishSpawns)
            {
                if ((fishSpawn.transform.position - spawnPosition).magnitude < minDistance) { invalidSpawn = true; }
            }

            if (invalidSpawn)
            {
                if (tries > 0)
                {
                    TryGetSpawnPosition();
                }
                else
                {
                    isSuccessfull = false;
                }
            }
            else
            {
                currentSpawnPosition = spawnPosition;
                isSuccessfull = true;
            }
        }

        TryGetSpawnPosition();

        if (isSuccessfull)
        {
            FishSpawn newFishSpawn = Instantiate(fishSpawnPrefab);
            newFishSpawn.fishSpawnSpawner = this;
            newFishSpawn.transform.position = currentSpawnPosition;
            
            // Rarity
            float distance = Vector3.Distance(currentSpawnPosition, startPosition);
            float maxDistance = Mathf.Min(transform.localScale.x, transform.localScale.z) * .9f;
            float rarity = Mathf.Clamp(distance / maxDistance * 10, 0, 10);
            
            // Decide fish using rarity
            //newFishSpawn.fish = ...
            
            fishSpawns.Add(newFishSpawn);
        }

        return isSuccessfull;
    }



    void Start()
    {
        int spawnFishSpawnAmount = Convert.ToInt32(transform.localScale.x * transform.localScale.z / (minDistance * 10));

        for (int i = 0; i < spawnFishSpawnAmount; i++)
        {
            bool isSuccessfull = SpawnFishSpawn();
            if (!isSuccessfull) { break; }
        }
    }
}
