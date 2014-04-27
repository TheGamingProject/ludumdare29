using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public float attackDamage = 1.0f;
	public float attackRate = 1.0f;

	private bool attacking = false;
	private float attackCooldown;
	private GameObject body;

	void Start() {
		body = GameObject.Find("Scripts");
	}

	void Update() {
		// cooldown
		if (attackCooldown > 0) {
			attackCooldown -= Time.deltaTime;
		}

		if (attacking && CanAttack) {
			attackCooldown = attackRate;
			// do damage to the body
			body.BroadcastMessage("DamageBody", attackDamage);
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
