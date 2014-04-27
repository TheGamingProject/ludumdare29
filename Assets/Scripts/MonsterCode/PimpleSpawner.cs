using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PimpleSpawner : MonoBehaviour {
	public Transform spawneePrefab;
	public Vector2 spawnXRange = new Vector2(-7.0f, 7.0f);
	public float spawnY = -1.66f;
	public int spawnCap = 5;
	public float spawnRate = 10.0f;

	private float spawnCooldown;
	private float constant = .8f;
	
	
	void Start () {
		
	}
	
	// we want to spawn them slowly at first and more over time, randomly on the screen (left to right)
	void Update () {
		// cooldown
		if (spawnCooldown > 0) {
			spawnCooldown -= Time.deltaTime;
		}
		
		if (CanSpawn && SpawnedAmount <= spawnCap) {
			spawnCooldown = spawnRate;
			spawnRate *= constant;
			
			float randomX = Random.Range(spawnXRange.x, spawnXRange.y);
			Transform newMosquito = (Transform) Instantiate(spawneePrefab, new Vector3(randomX, spawnY, 0f), transform.rotation);
			newMosquito.parent = transform;
		}
	}
	
	public bool CanSpawn {
		get{
			return spawnCooldown <= 0f;
		}
	}
	public List<Transform> getSpawned () {
		List<Transform> list = new List<Transform>();
		
		foreach (Transform child in transform) {
			if(child.name == "Pimple(Clone)"){
				list.Add(child);
			}
		}
		
		return list;
	}
	
	public int SpawnedAmount {
		get{
			return getSpawned().Count;
		}
	}

	public void StopSpawning () {
		spawnRate = 10000000;
	}
}