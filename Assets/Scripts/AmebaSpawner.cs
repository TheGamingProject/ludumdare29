using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmebaSpawner : MonoBehaviour {
	public Transform spawneePrefab;
	public float spawnX = 10.0f;
	public float spawnY = 2.75f;
	public float spawnRate = 10.0f;
	
	private float spawnCooldown;
	private int spawnedCount = 0;
	private float speedConstant = 0.01f; 
	
	
	void Start () {
		spawnCooldown = spawnRate;
	}
	
	// we want to spawn them slowly at first and more over time, randomly on the screen (left to right)
	void Update () {
		// cooldown
		if (spawnCooldown > 0) {
			spawnCooldown -= Time.deltaTime;
		}
		
		if (CanSpawn) {
			spawnCooldown = spawnRate;
			spawnedCount++;

			int leftOrRight = Random.Range(1, 3);
			float randomX = (float) leftOrRight == 1 ? spawnX : spawnX * -1;

			Transform newMosquito = (Transform) Instantiate(spawneePrefab, new Vector3(randomX, spawnY, 0f), transform.rotation);
			newMosquito.parent = transform;
			AmebaMoveScript ams = newMosquito.GetComponent<AmebaMoveScript>();
			float newSpeed = ams.speed + spawnedCount * speedConstant;
			ams.speed = leftOrRight == 1 ? newSpeed * -1 : newSpeed;
		}
	}
	
	public bool CanSpawn {
		get{
			return spawnCooldown <= 0f;
		}
	}

	public void StopSpawning () {
		spawnRate = 10000000;
	}
}
