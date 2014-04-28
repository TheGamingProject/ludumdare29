using UnityEngine;
using System.Collections;

public class EggScript : MonoBehaviour {
	public float crackRate = 1.0f;

	public float hatchRate = 2.0f;

	public float deathRate = 5.0f;

	private float crackCooldown;
	private float hatchCooldown;
	private float deathCooldown;

	public Transform spawneePrefab;

	void Start () {
		crackCooldown = crackRate;
		hatchCooldown = hatchRate;
	}

	void Update() {
		// cooldown
		if (crackCooldown > 0) {
			crackCooldown -= Time.deltaTime;

			if (crackCooldown <= 0) {
				GetComponent<EggAnimationScript>().changeStateToHatch1();
			}
		}

		if (hatchCooldown > 0) {
			hatchCooldown -= Time.deltaTime;

			if (hatchCooldown <= 0) {
				hatchEgg();
			}
		}

		if (deathCooldown > 0) {
			deathCooldown -= Time.deltaTime;

			if (deathCooldown <= 0) {
				Destroy(gameObject);
			}
		}

	}

	void hatchEgg () {
		GetComponent<EggAnimationScript>().changeStateToHatch2();

		Vector3 pos1 = new Vector3(transform.position.x - .75f, transform.position.y, transform.position.z);
		//Vector3 pos2 = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
		Vector3 pos3 = new Vector3(transform.position.x + .75f, transform.position.y, transform.position.z);
		
		Transform newMosquito1 = (Transform) Instantiate(spawneePrefab, pos1, transform.rotation);
		newMosquito1.parent = transform.parent;
		
		//Transform newMosquito2 = (Transform) Instantiate(spawneePrefab, pos2, transform.rotation);
		//newMosquito2.parent = transform.parent;
		
		Transform newMosquito3 = (Transform) Instantiate(spawneePrefab, pos3, transform.rotation);
		newMosquito3.parent = transform.parent;

		deathCooldown = deathRate;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		// Collision with boss
		KingMosquitoScript boss = collider.gameObject.GetComponent<KingMosquitoScript>();
		if (boss != null) {
			boss.HitByEgg();
		}
	}

	public void HitByPlayer () {
		if (deathCooldown <= 0) {
			GetComponent<EggFlyScript>().BlastOff();
		}
	}

}
