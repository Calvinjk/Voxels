using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject thingToSpawn;
    public float frequency          = 1f;           //After how much time does a new object spawn?
    public float spawnOffset        = 0f;           //How much variation is there in the spawns
    public bool  activated          = true;         //Should we be currently spawning objects?  
    public bool  randomize          = false;        //Should the spawner not spawn at exactly regular intervals?
    public float randomOffset       = .5f;          //If the spawner is spawning randomly, how much randomness is involved?
    
    private float curSpawnTimer     = 0f;
    private float variation;

    // Use this for initialization
    void Start () {
        variation = randomOffset + 1f;  //This will be the default value beacuse Random.Range will never equal it
	}
	
	// Update is called once per frame
	void Update () {
	    if (activated) {                            //Make sure we are able to turn the spawner on and off
            curSpawnTimer += Time.deltaTime;        //Increment the timer
            if (randomize) { //If randomize is enabled, put some variation on the spawns
                if (variation == randomOffset + 1f) {
                    variation = Random.Range(-randomOffset, randomOffset);
                }
                
                if (curSpawnTimer > (frequency + variation)) {
                    SpawnObject();
                }
            } else {
                if (curSpawnTimer > frequency) {
                    SpawnObject();
                }
            }
        }
	}

    void SpawnObject() {
        //Reset variables
        curSpawnTimer = 0f;
        variation = randomOffset + 1f; ;

        Vector3 pos = transform.position;
        Vector3 spawnPos = new Vector3(pos.x + Random.Range(-spawnOffset, spawnOffset), pos.y, pos.z + Random.Range(-spawnOffset, spawnOffset));
        GameObject obj = Instantiate(thingToSpawn, spawnPos, Quaternion.identity) as GameObject;
        obj.name = thingToSpawn.name;
    }
}
