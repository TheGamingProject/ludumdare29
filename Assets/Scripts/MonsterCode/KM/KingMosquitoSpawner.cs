using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KingMosquitoSpawner : MonoBehaviour {
	public Transform spawneePrefab;
	public float spawnX = 0.0f;
	public float spawnY = 8.0f;

	public int spawnAtPointsAmount = 100;
			
	private bool spawned = false;
	private PointManagerScript pms; 

	void Start () {
		pms = GameObject.Find("Scripts").GetComponent<PointManagerScript>();
	}

	void Update () {
		if (!spawned && CanSpawn) {
			spawned = true;
			Transform newMosquito = (Transform) Instantiate(spawneePrefab, new Vector3(spawnX, spawnY, 0f), transform.rotation);
			newMosquito.parent = transform;
		}
	}
	
	public bool CanSpawn {
		get{
			return pms.GetCurrentPoints() >= spawnAtPointsAmount;
		}
	}

}
