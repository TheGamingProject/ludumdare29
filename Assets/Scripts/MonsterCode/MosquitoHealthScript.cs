using UnityEngine;
using System.Collections;

public class MosquitoHealthScript : MonoBehaviour {
	public float totalHP = 1;
	public float invulnTime = .5f; //seconds
	public int pointsAwarded = 1;

	public float deathTime = .5f;

	private float currentHp;
	private float invlunCooldown;

	private bool death = false;
	private float deathCooldown;
	
	void Start() {
		currentHp = totalHP;
	}

	void Update() {
		if (invlunCooldown > 0) {
			invlunCooldown -= Time.deltaTime;

			if (invlunCooldown <= 0) {
				GetComponent<MosquitoAnimationScript>().changeStateToLanded();
			}
		}

		if (death && deathCooldown > 0) {
			deathCooldown -= Time.deltaTime;
		}

		if (death && deathCooldown <= 0) {
			
			Destroy(gameObject);
		}
	}

	public void Damage(float amount) {
		if (!isNotInvlun) return;

		currentHp -= amount;


		invlunCooldown = invulnTime;

		if (currentHp <= 0) {
			//Debug.Log("KILLED enemy");
			GameObject.Find("Scripts").BroadcastMessage("GivePoints", pointsAwarded);

			death = true;
			GetComponent<MosquitoAnimationScript>().changeStateToDead();
			deathCooldown = deathTime;
		} else {
			GetComponent<MosquitoAnimationScript>().changeStateToInvun();
		}
			
	}

	bool isNotInvlun {
		get{
			return invlunCooldown <= 0f;
		}
	}

}
