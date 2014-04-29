using UnityEngine;
using System.Collections;

// controls boss states and movement
//  tells KingMosquitoScript when he can attack
public class KingMosquitoFlyScript : MonoBehaviour{
	public enum flightStates {
		flyToPrephase, waitAtPrephase, flyUpFromPrephase,
		flyDownToPhase1, flySideToSide, phaseShift1Wait, flyUpFromPhase1,
		flyDownToPhase2, phase2Landed, phaseShift2Wait, flyUpFromPhase2,
		endBossFight
	};

	public int eggPhases = 3;
	private int currentEggPhases = 0;

	public Vector2 flyOffScreenLocation = new Vector2(0.0f, 10.0f);
	//prephase
	public float flyDownToPreTime = .25f;
	public Vector2 flyDownToPreLocation = new Vector2 (0.0f, 2.0f);
	public float prephaseFlyingWaitTime = 2.5f;

	private float prephaseWaitCooldown;
	public float prephaseToUpTime = .25f; //flying to off screen location

	//phase1
	public float flyToPhase1LocationTime = 1.0f;
	public Vector2 phase1StartingLocation = new Vector2(0, 2.2f);
	public Vector2 sideToSideXRange = new Vector2(-6,-6);
	public float sideToSideSpeed = 10.0f;
	private int sideToSideDirection;
	public float phaseShift1WaitRate = .25f;
	private float phaseShift1Cooldown;
	public float phase1PerRoundSpeedMultiplier = .5f;

	//phase2

	public float flyToPhase2LocationTime = 10.0f;
	public Vector2 phase2StartingLocation = new Vector2(0.0f, 0.0f);
	public Transform greenMosquitoPrefab;
	public int greenMosquitoPacksDuringPhase2Intro = 4;
	private int packsPerIntroCounter = 0; 
	public int introPackSize = 10;
	public float packsStartY = 7.0f;
	public Vector2 packsStartXRange = new Vector2(-.6f, 6.0f);
	public float introPacksRate = 2.5f;
	private float introPacksCooldown;
	private float prevBoxcolliderValue;
	public float phaseShift2WaitRate = .25f;
	private float phaseShift2Cooldown;
	
	public float packsPerRoundMultiplier = 1.00f; 

	//km death
	public Vector2 deathLocation = new Vector2(0, 2);
	public float deathFlightTime = .15f;


	private Vector2 flightLength;

	private flightStates flightState;
	private flightStates CurrentState {
		get {
			return flightState;
		}
		set {
			flightState = value;
		}
	}

	private KingMosquitoAnimationScript animationScript;

	private bool flying;
	private Vector2 movement;
	private BoxCollider2D boxCollider;
	
	void Start () {
		animationScript = GetComponent<KingMosquitoAnimationScript>();
		animationScript.changeStateToFlyingDown();
		SetPhaseTo(flightStates.flyToPrephase);
		flightLength = new Vector2(flyDownToPreLocation.x - transform.position.x, flyDownToPreLocation.y - transform.position.y);
		flying = true;

		boxCollider = GetComponent<BoxCollider2D>();
		GameObject.Find("Scripts").GetComponent<MusicPlayerScript>().TriggerBossMusic();
	}
	
	void Update () {

		switch (flightState) {
		case flightStates.flyToPrephase: 
			movement = new Vector2(flightLength.x / flyDownToPreTime, flightLength.y / flyDownToPreTime);
			break;
		}

	}
	
	
	void FixedUpdate() {
		if (flying) {
			rigidbody2D.velocity = movement;
		}

		switch (flightState) {
		case flightStates.flyToPrephase: 
			if (transform.position.y < flyDownToPreLocation.y) {
				StartEndPrephase();
			}
			break;
		case flightStates.waitAtPrephase:
			if (prephaseWaitCooldown > 0) {
				prephaseWaitCooldown -= Time.deltaTime;
			} else {
				FlyUpToAboveOffscreenLocation();
				SetPhaseTo(flightStates.flyUpFromPrephase);
			}
			break;
		case flightStates.flyUpFromPrephase: 
			if (transform.position.y > flyOffScreenLocation.y) {
				EndPrephase();
			}
			break;
		case flightStates.flyDownToPhase1:
			if (transform.position.y < phase1StartingLocation.y) {
				StartPhase1();
			}
			break;
		case flightStates.flySideToSide:
			//fly side to side
			float newSpeed = sideToSideSpeed * (1 + (currentEggPhases) * phase1PerRoundSpeedMultiplier);

			float newX = transform.position.x + sideToSideDirection * newSpeed * Time.deltaTime;
			float newY = Mathf.Sin(5 * newX) * .5f + phase1StartingLocation.y;

			if (sideToSideDirection == 1 && newX > sideToSideXRange.y) {
				sideToSideDirection = -1;
			} else if (sideToSideDirection == -1 && newX < sideToSideXRange.x) {
				sideToSideDirection = 1;
			}

			transform.position = new Vector3(newX, newY, transform.position.z);

			break;
		case flightStates.phaseShift1Wait:
			if (phaseShift1Cooldown > 0) {
				phaseShift1Cooldown -= Time.deltaTime;
			} else {
				FlyUpToAboveOffscreenLocation();
				SetPhaseTo(flightStates.flyUpFromPhase1);
			}
			break;
		case flightStates.flyUpFromPhase1: 
			if (transform.position.y > flyOffScreenLocation.y) {
				FlyDownToPhase2();
			}
			break;
		case flightStates.flyDownToPhase2:
			//spawn more packs:
			if (introPacksCooldown > 0 && packsPerIntroCounter > 0) {
				introPacksCooldown -= Time.deltaTime;

				if (introPacksCooldown <= 0) {
					packsPerIntroCounter--;
					introPacksCooldown = introPacksRate;
					spawnGreenMosquitoPacks();
				}
			}

			//check if landed
			if (transform.position.y < phase2StartingLocation.y) {
				StartPhase2();
			}
			break;
		case flightStates.phase2Landed:

			break;
		case flightStates.phaseShift2Wait:
			if (phaseShift2Cooldown > 0) {
				phaseShift2Cooldown -= Time.deltaTime;
			} else {
				FlyUpToAboveOffscreenLocation();
				SetPhaseTo(flightStates.flyUpFromPhase2);
			}
			break;
		case flightStates.flyUpFromPhase2:
			if (transform.position.y > flyOffScreenLocation.y) {
				FlyDownToPhase1();
			}
			break;
		case flightStates.endBossFight:
			if (transform.position.y > deathLocation.y) {
				ExplodeKingMosquito();
			}
			break;
		}

		
	}

	void SetPhaseTo (flightStates fs) {
		CurrentState = fs;
		Debug.Log("Changing State to: " + fs.ToString());
	}

	void FlyUpToAboveOffscreenLocation () {
		flying = true;
		animationScript.changeStateToFlyingUp();
		flightLength = new Vector2(flyOffScreenLocation.x - transform.position.x, flyOffScreenLocation.y - transform.position.y);
		movement = new Vector2(flightLength.x / prephaseToUpTime, flightLength.y / prephaseToUpTime);
	}

	void StartEndPrephase () {
		StopFlying();
		SetPhaseTo(flightStates.waitAtPrephase);
		prephaseWaitCooldown = prephaseFlyingWaitTime;
	}

	void EndPrephase () {
		// now off the top of the screen
		FlyDownToPhase1();
	}

	void FlyDownToPhase1 () {
		SetPhaseTo(flightStates.flyDownToPhase1);
		animationScript.changeStateToPhase1Flying();
		flightLength = new Vector2(phase1StartingLocation.x - transform.position.x, phase1StartingLocation.y - transform.position.y);
		movement = new Vector2(flightLength.x / flyToPhase1LocationTime, flightLength.y / flyToPhase1LocationTime);
	}

	void StartPhase1 () {
		StopFlying ();
		SetPhaseTo(flightStates.flySideToSide);
		sideToSideDirection = Random.Range(0,2) == 1 ? 1 : -1;
		GetComponent<KingMosquitoScript>().StartPhase1();
	}

	public void EndPhase1 () {
		animationScript.changeStateToPhaseShift1();
		GetComponent<HashAudioScript>().PlayAudio("KM_ps");
		StopFlying ();
		SetPhaseTo(flightStates.phaseShift1Wait);
		phaseShift1Cooldown = phaseShift1WaitRate;
		currentEggPhases++;
	}

	void FlyDownToPhase2 () {
		SetPhaseTo(flightStates.flyDownToPhase2);
		animationScript.changeStateToFlyingDown();
		// fly down slowly and spawn green mosquitios

		flying = true;
		flightLength = new Vector2(phase2StartingLocation.x - transform.position.x, phase2StartingLocation.y - transform.position.y);
		movement = new Vector2(flightLength.x / flyToPhase2LocationTime, flightLength.y / flyToPhase2LocationTime);

		introPacksCooldown = introPacksRate;
		packsPerIntroCounter = greenMosquitoPacksDuringPhase2Intro;
		packsPerIntroCounter *= Mathf.RoundToInt(1 + (currentEggPhases - 1) * packsPerRoundMultiplier);
		//Debug.Log("packs per intro counter: " + packsPerIntroCounter);
	}

	void StartPhase2 () {
		SetPhaseTo(flightStates.phase2Landed);
		StopFlying();
		animationScript.changeStateToLanded();
		GetComponent<KingMosquitoScript>().StartPhase2();
		GetComponent<HashAudioScript>().PlayAudio("KM_landed");

		// move boxcollider down
		prevBoxcolliderValue = boxCollider.center.y;
		boxCollider.center = new Vector2(boxCollider.center.x, -1.1f);
	}

	public void EndPhase2 () {
		if (currentEggPhases >= eggPhases) {
			//win
			EndBossFight();
			return;
		} 

		animationScript.changeStateToPhaseShift2();
		SetPhaseTo(flightStates.phaseShift2Wait);
		phaseShift2Cooldown = phaseShift2WaitRate;
		GetComponent<HashAudioScript>().PlayAudio("KM_ps2");

		// move boxcollider back up
		boxCollider.center = new Vector2(boxCollider.center.x, prevBoxcolliderValue);
	}

	public void EndBossFight () {
		SetPhaseTo(flightStates.endBossFight);
		animationScript.changeStateToDead(); 

		flying = true;
		flightLength = new Vector2(deathLocation.x - transform.position.x, deathLocation.y - transform.position.y);
		movement = new Vector2(flightLength.x / deathFlightTime, flightLength.y / deathFlightTime);
	}

	void ExplodeKingMosquito () {
		StopFlying();

		animationScript.explodeDeathAnimation();
		GetComponent<HashAudioScript>().PlayAudio("KM_death");
		Destroy(gameObject);
	}

	void spawnGreenMosquitoPacks () {
		for (int i = 0; i < introPackSize; i++) {
			float randomX = Random.Range(packsStartXRange.x, packsStartXRange.y);
			Transform newMosquito = (Transform) Instantiate(greenMosquitoPrefab, new Vector3(randomX, packsStartY, 0f), transform.rotation);
			newMosquito.parent = transform.parent;
		}
	}
	
	void StopFlying () {
		flying = false;
		rigidbody2D.velocity = new Vector2(0,0);
		//GetComponent<MosquitoAnimationScript>().changeStateToLanded();
	}
}

