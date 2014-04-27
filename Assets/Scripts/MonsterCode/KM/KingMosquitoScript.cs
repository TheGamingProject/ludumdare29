using UnityEngine;
using System.Collections;


// controls states, attacking, and movement
public class KingMosquitoScript : MonoBehaviour {
	public enum states {
		prephase, phase1, phase2
	}

	public int eggPhases = 3;
	//prephase
	  // stop all other flies from spawning
	  // spawner flies it into position
	  
	  //then flies back up off the screen  

	//phase 1
	  // KM flies down from top of screen
	  // KM flies accross top of the screen
	public Vector2 leftAndRightMovementBounds = new Vector2(-6, 6);
	public float leftAndRightMovementSpeed = 7.5f;
	public float spawnEggRate = 2.0f;
	public int eggHitsTilPhaseChange = 3;
	  // KM flies off the up off the screen

	//phase 2
	  // KM flies down from top of screen to the ground
	public float damageTilEggPhase = 20.0f; // or death

	public float attackDamage = 5.0f;
	public float attackRate = 1.0f;
	public float suckingRate = .25f;
	// KM flies up off the screen 

	// end custom vars

	private bool attacking = false;
	private float attackCooldown;
	private float suckingCooldown;
	private GameObject body;
	
	void Start() {
		body = GameObject.Find("Scripts");

		StartPrephase();
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

	void StartPrephase () {
		foreach(MosquitoSpawner ms in transform.parent.GetComponents<MosquitoSpawner>()) {
			ms.StopSpawning();
		}
	}

	void StartPhase1 () {

	}

	void StartPhase2 () {

	}

	void Die () {
		foreach(MosquitoSpawner ms in transform.parent.GetComponents<MosquitoSpawner>()) {
			ms.StartSpawning();
		}
	}
}
