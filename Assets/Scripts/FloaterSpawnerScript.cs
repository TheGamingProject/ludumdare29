using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloaterSpawnerScript : MonoBehaviour {
	public Transform spawneePrefab;
	public float spawnX = 10.0f;
	public Vector2 spawnYRange = new Vector2(7, 1);
	public Vector2 spawnRateRange = new Vector2(2, 16);
	
	private float spawnCooldown;

	
	void Start () {

	}
	
	// we want to spawn them slowly at first and more over time, randomly on the screen (left to right)
	void Update () {
		// cooldown
		if (spawnCooldown > 0) {
			spawnCooldown -= Time.deltaTime;
		}
		
		if (CanSpawn) {
			spawnCooldown = Random.Range(spawnRateRange.x, spawnRateRange.y);

			int leftOrRight = Random.Range(1, 3);
			float randomX = (float) leftOrRight == 1 ? spawnX : spawnX * -1;
			float randomY = Random.Range(spawnYRange.x, spawnYRange.y);

			Transform newFloater = (Transform) Instantiate(spawneePrefab, new Vector3(randomX, randomY, 0f), transform.rotation);
			newFloater.parent = transform;
			FloaterMoveScript ams = newFloater.GetComponent<FloaterMoveScript>();
			ams.leftOrRight = leftOrRight;
		}
	}
	
	public bool CanSpawn {
		get{
			return spawnCooldown <= 0f;
		}
	}
}
