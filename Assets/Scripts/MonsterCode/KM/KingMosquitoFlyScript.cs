using UnityEngine;
using System.Collections;

// controls boss states and movement
//  tells KingMosquitoScript when he can attack
public class KingMosquitoFlyScript : MonoBehaviour{
	public enum flightStates {
		flyToPrephase, waitAtPrephase, flyUpFromPrephase,
		flyDownToPhase1, flySideToSide, flyUpFromPhase1,
		flyDownToPhase2, flyUpFromPhase2
	};


	public Vector2 flyOffScreenLocation = new Vector2(0.0f, 10.0f);
	//prephase
	public float flyDownToPreTime = .25f;
	public Vector2 flyDownToPreLocation = new Vector2 (0.0f, 2.0f);
	public float prephaseFlyingWaitTime = 2.5f;

	private float prephaseWaitCooldown;
	public float prephaseToUpTime = .25f; //flying to off screen location

	//phase1
	public Vector2 phase2StartingLocation = new Vector2(0, 5.0f);

	//phase2

	// fly up

	// fly to ground
/*	public Vector2 skinLevelRange;
*/
//	private float flightTime;
	private Vector2 flightLength;
//	private float skinLevel;

	private flightStates flightState;
	private flightStates CurrentState {
		get {
			return flightState;
		}
		set {
			flightState = value;
		}
	}

	private bool flying;
	private Vector2 movement;
	
	void Start () {
		GetComponent<KingMosquitoAnimationScript>().changeStateToFlyingDown();
		SetPhaseTo(flightStates.flyToPrephase);
		flightLength = new Vector2(flyDownToPreLocation.x - transform.position.x, flyDownToPreLocation.y - transform.position.y);
		flying = true;
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
		}

		
	}

	void SetPhaseTo (flightStates fs) {
		CurrentState = fs;
		Debug.Log("Changing State to: " + fs.ToString());
	}

	void FlyUpToAboveOffscreenLocation () {
		flying = true;
		GetComponent<KingMosquitoAnimationScript>().changeStateToFlyingUp();
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
		StopFlying();
		SetPhaseTo(flightStates.flyDownToPhase1);
	}
	
	void StopFlying () {
		flying = false;
		rigidbody2D.velocity = new Vector2(0,0);
		//GetComponent<MosquitoAnimationScript>().changeStateToLanded();
	}
}

