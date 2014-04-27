using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MosquitoSpawner : MonoBehaviour {
	public Transform spawneePrefab;
	public Vector2 spawnXRange = new Vector2(-7.2f, 8.0f);
	public float spawnY = 7.0f;
	public int spawnCap = 20;
	public float spawnRate = 2.0f;
	public float baseSpawnRate = 1.0f;

	public int spawnAmount = 1;
	public float initialSpawnCooldown = 2.0f;

	private float spawnCooldown;
	private float constant = .98f;

	public bool spawning = true;

	void Start () {
		spawnCooldown = initialSpawnCooldown;
	}

	// we want to spawn them slowly at first and more over time, randomly on the screen (left to right)
	void Update () {
		// cooldown
		if (spawnCooldown > 0) {
			spawnCooldown -= Time.deltaTime;
		}

		if (CanSpawn && SpawnedAmount <= spawnCap) {
			spawnCooldown = spawnRate + baseSpawnRate;
			spawnRate *= constant;

			for (int i = 0; i < spawnAmount; i++) {
				float randomX = Random.Range(spawnXRange.x, spawnXRange.y);
				Transform newMosquito = (Transform) Instantiate(spawneePrefab, new Vector3(randomX, spawnY, 0f), transform.rotation);
				newMosquito.parent = transform;
			}
		}
	}

	public bool CanSpawn {
		get{
			return spawning && spawnCooldown <= 0f;
		}
	}
	public List<Transform> getSpawned () {
		List<Transform> list = new List<Transform>();

		foreach (Transform child in transform) {
			if(child.name == spawneePrefab.name + "(Clone)"){
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

	public void StartSpawning () {
		spawning = true;
		spawnCooldown = 5;
	}

	public void StopSpawning () {
		spawning = false;
	}
}
