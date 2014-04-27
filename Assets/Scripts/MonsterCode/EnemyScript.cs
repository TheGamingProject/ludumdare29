using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public float attackDamage = 1.0f;
	public float attackRate = 1.0f;

	public float suckingRate = .2f;

	private bool attacking = false;
	private float attackCooldown;
	private float suckingCooldown;
	private GameObject body;

	void Start() {
		body = GameObject.Find("Scripts");
	}

	void Update() {
		// cooldown
		if (attackCooldown > 0) {
			attackCooldown -= Time.deltaTime;
		}

		if (suckingCooldown > 0) {
			suckingCooldown -= Time.deltaTime;

			if (suckingCooldown <= 0) {
				GetComponent<MosquitoAnimationScript>().changeStateToLanded();
			}
		}

		if (attacking && CanAttack) {
			attackCooldown = attackRate;
			suckingCooldown = suckingRate;
			// do damage to the body
			body.BroadcastMessage("DamageBody", attackDamage);
			GetComponent<MosquitoAnimationScript>().changeStateToSucking();
		}
	}

	public bool CanAttack {
		get{
			return attackCooldown <= 0f;
		}
	}

	public void StartAttacking () {
		attacking = true;
	}
}
