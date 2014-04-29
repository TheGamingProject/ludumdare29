using UnityEngine;
using System.Collections;


// controls states, attacking, and movement
public class KingMosquitoScript : MonoBehaviour {
	public enum states {
		flying, prephase, phase1, phase2
	}
	private states state;
	private int round = 0;

	//prephase
	  // stop all other flies from spawning
	  // spawner flies it into position
	  
	  //then flies back up off the screen  

	//phase 1
	  // KM flies down from top of screen
	  // KM flies accross top of the screen
	public Vector2 spawnEggRateRange = new Vector2(1.5f, 2.5f);
	public float spawningEggAnimationRate = .2f;
	public int eggHitsTilPhaseChange = 3;
	public Transform eggPrefab;
	public float phase1InvunRate = .25f;

	public float eggSpawnDifficultyPerRound = .5f;

	private int eggHitsThisPhase = 0;
	private float eggSpawnCooldown;
	private float eggSpawningCooldown;
	private float phase1InvunCooldown; 

	  // KM flies off the up off the screen

	//phase 2
	  // KM flies down from top of screen to the ground
	public float damageTilEggPhase2 = 20.0f; // or death
	private float healthTilEggPhase2 = 0;

	public float attackDamage = 5.0f;
	public float attackRate = 1.0f;
	public float suckingRate = .25f;

	public float phase2InvunRate = .25f;
	private float phase2InvunCooldown;
	// KM flies up off the screen 

	// end custom vars

	private bool attacking = false;
	private float attackCooldown;
	private float suckingCooldown;
	private GameObject body;
	
	void Start() {
		body = GameObject.Find("Scripts");
		state = states.prephase;

		StartPrephase();
	}
	
	void Update() {
		if (state == states.phase1) {
			if (eggSpawnCooldown > 0) {
				eggSpawnCooldown -= Time.deltaTime;
			}

			if (eggSpawningCooldown > 0) {
				eggSpawningCooldown -= Time.deltaTime;
				
				if (eggSpawningCooldown <= 0) {
					GetComponent<KingMosquitoAnimationScript>().changeStateToPhase1Flying();
				}
			}

			if (CanSpawnEgg) {
				eggSpawnCooldown = Random.Range(spawnEggRateRange.x, spawnEggRateRange.y);
				eggSpawnCooldown /= 1 + (round - 1) * eggSpawnDifficultyPerRound;
				Debug.Log("egg cooldown: " + eggSpawnCooldown);
				eggSpawningCooldown = spawningEggAnimationRate;

				SpawnEgg(); 
			}

			if (phase1InvunCooldown > 0) {
				phase1InvunCooldown -= Time.deltaTime;
				
				if (phase1InvunCooldown <= 0) {
					GetComponent<KingMosquitoAnimationScript>().changeStateToPhase1Flying();
				}
			}
		} else  if (state == states.phase2) {
			if (attackCooldown > 0) {
				attackCooldown -= Time.deltaTime;
			}
			
			if (suckingCooldown > 0) {
				suckingCooldown -= Time.deltaTime;
				
				if (suckingCooldown <= 0) {
					GetComponent<KingMosquitoAnimationScript>().changeStateToLanded();
				}
			}
			
			if (attacking && CanAttack) {
				attackCooldown = attackRate;
				suckingCooldown = suckingRate;
				// do damage to the body
				body.BroadcastMessage("DamageBody", attackDamage);
				GetComponent<KingMosquitoAnimationScript>().changeStateToSucking();
			}

			if (healthTilEggPhase2 <= 0) {
				GetComponent<KingMosquitoFlyScript>().EndPhase2();
				attacking = false;
				state = states.flying;
			}

			if (phase2InvunCooldown > 0) {
				phase2InvunCooldown -= Time.deltaTime;
				
				if (phase2InvunCooldown <= 0) {
					GetComponent<KingMosquitoAnimationScript>().changeStateToLanded();
				}
			}
		}
	}
	
	bool CanAttack {
		get{
			return attackCooldown <= 0f;
		}
	}

	bool CanSpawnEgg {
		get{
			return eggSpawnCooldown <= 0f;
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

	public void StartPhase1 () {
		state = states.phase1;
		eggHitsThisPhase = 0;
		round++;
	}

	public void StartPhase2 () {
		state = states.phase2;
		StartAttacking();
		healthTilEggPhase2 = damageTilEggPhase2;
	}

	public void StopAttacks () {
		state = states.flying;
	}

	void SpawnEgg () {
		Transform newMosquito = (Transform) Instantiate(eggPrefab, transform.position, transform.rotation);
		newMosquito.parent = transform.parent;
		GetComponent<KingMosquitoAnimationScript>().changeStateToPhase1Attacking();

		GetComponent<HashAudioScript>().PlayAudio("KM_eggshooter");
	}

	public void HitByEgg () {
		eggHitsThisPhase++;

		if (eggHitsThisPhase >= (eggHitsTilPhaseChange + (round - 1))) {
			StopAttacks();
			GetComponent<KingMosquitoFlyScript>().EndPhase1();
		} else {
			phase1InvunCooldown = phase1InvunRate;
			GetComponent<KingMosquitoAnimationScript>().changeStateToFlyingInvun();
		}
	}

	public void HitByPlayer (float amount) {
		if (state != states.phase2) return;

		healthTilEggPhase2 -= amount;
		GetComponent<KingMosquitoAnimationScript>().changeStateToInvun();
		phase2InvunCooldown = phase2InvunRate;
		
		GetComponent<HashAudioScript>().PlayAudio("KM_egghitter");
	}

	void Die () {
		foreach(MosquitoSpawner ms in transform.parent.GetComponents<MosquitoSpawner>()) {
			ms.StartSpawning();
		}
	}
}
