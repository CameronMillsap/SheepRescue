using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true; //true will spawn sheep

    public GameObject sheepPrefab; // refrence to sheepPrefab
    public List<Transform> sheepSpawnPositions = new List<Transform>(); // positions where the sheep are spawned
    public float timeBetweenSpawns; //time in seconds between spawning

    private List<GameObject> sheepList = new List<GameObject>(); //a list of all alive sheep

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position; //choses a random spawner 0, 1, 2
        GameObject sheep  = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation); //create new sheep at the randomly chosen spawner
        sheepList.Add(sheep); //add refrence to sheep in the list
        sheep.GetComponent<Sheep>().SetSpawner(this); //adds refrence to the spawner for sheep to report to
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }

        sheepList.Clear();
    }
}
