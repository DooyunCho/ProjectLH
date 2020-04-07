using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject monster;
    public bool respawn;
    public float spawnDelay;
    private float currentTime;
    private bool spawning;

	// Use this for initialization
	void Start () {
        Spawn();
        currentTime = spawnDelay;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.position, transform.up * 5f, Color.blue);

        if (spawning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                Spawn();
            }
        }
	}

    public void Respawn()
    {
        spawning = true;
        currentTime = spawnDelay;
    }

    void Spawn()
    {
        IEnemy instance = Instantiate(monster, transform.position, Quaternion.identity).GetComponent<IEnemy>();
        instance.Spawner = this;
        spawning = false;
    }
}
