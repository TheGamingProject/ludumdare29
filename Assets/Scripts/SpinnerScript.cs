using UnityEngine;
using System.Collections;

public class SpinnerScript : MonoBehaviour {
	private float travelTime = 10.0f;
	private Vector2 movement;

	GameObject childObject;
	private float spinSpeed;
	Rigidbody2D rod;

	private bool stop = false, stopped = false;

	void Update () {
		// hard coding crown stop location
		if (stop && transform.position.y < -.4f) {
			movement = new Vector2(0,0);
			transform.position = new Vector3(0f,0.4f,0f);
			stopped = true;
		} 
	}

	public void StartSpin (Sprite sprite, float _spinSpeed, Vector2 spinLocation, bool _stop) {
		childObject = new GameObject();
		childObject.AddComponent<SpriteRenderer>();
		childObject.GetComponent<SpriteRenderer>().sprite = sprite;
		childObject.transform.parent = transform;

		stop = _stop;
		spinSpeed = _spinSpeed;

		gameObject.AddComponent<Rigidbody2D>();
		rod = gameObject.GetComponent<Rigidbody2D>();
		rod.gravityScale = 0;

		// start movement
		if (!_stop) {
			Vector2 flightLength = new Vector2(spinLocation.x - transform.position.x, spinLocation.y - transform.position.y);
			movement = new Vector2(flightLength.x / travelTime, flightLength.y / travelTime);
		}

	}

	void FixedUpdate() {
		if (!stopped)
			rod.velocity = movement;

		if (childObject != null)
			childObject.transform.Rotate(new Vector3(0f,0f, spinSpeed * Time.deltaTime));
	}
}
