using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameMaster handles game logic on a higher level; spawns enemies, ends and starts games, etc.

public class GameMaster : MonoBehaviour {
    // Creep GameObject
    public GameObject Creep;
    // Timer until next Creep spawn
    public float nextSpawn = 5.0f;
    public float timeSpawn = 10.0f;
    // Spawn distance from origin
    public float spawnDistance = 12.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Reduce time to next Creep spawn
        nextSpawn -= Time.deltaTime;
        if(nextSpawn < 0)
        {
            // Reset spawn timer
            this.nextSpawn = this.timeSpawn;
            // Spawn a Creep randomly to either side      
            if (Random.Range(0.0f, 1.0f) > 0.5f)
            {
                GameObject creep = GameObject.Instantiate(this.Creep);
                creep.transform.position = new Vector2(this.spawnDistance, 0);
            }
            else
            {
                GameObject creep = GameObject.Instantiate(this.Creep);
                creep.transform.position = new Vector2(-this.spawnDistance, 0);
            }
        }
    }
}
