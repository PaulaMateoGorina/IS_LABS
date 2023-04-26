using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{   
    // Whether the spawner can keep spawning sheep or not
    public bool canSpawn = true;

    // Prefab of the sheep
    public GameObject sheepPrefab; 
    // Positions where the sheep will be spawned
    public List<Transform> sheepSpawnPositions = new List<Transform>(); 
    // Cooldown for the spawning
    public float timeBetweenSpawns; 
    // All sheep that are alive at the moment
    private List<GameObject> sheepList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // To start a coroutine, we need to call this method with our coroutine as parameter
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnSheep()
    {
        // So that it spawns from one of the spawners in a random way
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position; // 1
        
        // Instantiate a sheep prefab
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation); // 2
        
        // Add it to the sheep list
        sheepList.Add(sheep); 

        // Add reference to the spawner for the sheep to report to
        sheep.GetComponent<Sheep>().SetSpawner(this); 
    }

    // This is a coroutine. Unlike normal methods, they can run accross multiple frames / seconds
    // We can pause / resume their execution
    private IEnumerator SpawnRoutine()
    {
        while (canSpawn) 
        {
            SpawnSheep(); 

            // Pause execution until the cooldown for spawning a new sheep is met
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    // Function to remove a sheep from the list
    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    // Destroy all sheeps in the scene
    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList) // 1
        {
            Destroy(sheep); // 2
        }

        sheepList.Clear();
    }

}
