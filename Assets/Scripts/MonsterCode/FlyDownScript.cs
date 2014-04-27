using UnityEngine;
using System.Collections;

public class FlyDownScript : MonoBehaviour{
	public Vector2 skinLevelRange;
	public Vector2 flyingTimeRange = new Vector2(.25f, .75f);
	public Vector2 flyingXRange = new Vector2(-.2f, .2f);

	public Sprite flyingSprite;
	public Sprite suckingSprite;

	private float flightTime;
	private Vector2 flightLength;
	private bool flying = true;
	private Vector2 movement;
	private float skinLevel;

	private BoxCollider2D boxCollider;

	void Start (){
		skinLevel = Random.Range(skinLevelRange.x, skinLevelRange.y);
		flightTime = Random.Range(flyingTimeRange.x, flyingTimeRange.y);
		float xLength = Random.Range(flyingXRange.x, flyingXRange.y);

		if (transform.position.x > 0)
			xLength -= 3;
		else
			xLength += 3;

		flightLength = new Vector2(xLength, skinLevel - transform.position.y);
		GetComponent<SpriteRenderer>().sprite = flyingSprite;

		
		boxCollider = (GetComponent("BoxCollider2D") as BoxCollider2D);

		if (xLength > 0) {
			// flip it
			transform.Rotate(new Vector3(0, 180, 0));
			boxCollider.center = new Vector2(boxCollider.center.x * -1, boxCollider.center.y);
		}

		boxCollider.enabled = false;
	}

	void Update (){

		if (flying) {
			movement = new Vector2(flightLength.x / flightTime, flightLength.y / flightTime);
		}
	}

	
	void FixedUpdate() {
		if (flying) {
			rigidbody2D.velocity = movement;

			if (transform.position.y < skinLevel) {
				StopFlying();
			}
 		}
	}

	void StopFlying () {
		transform.position = new Vector3(transform.position.x, skinLevel, transform.position.z);
		flying = false;
		rigidbody2D.velocity = new Vector2(0,0);
		transform.GetComponent<EnemyScript>().StartAttacking();
		GetComponent<SpriteRenderer>().sprite = suckingSprite;
		boxCollider.enabled = true;
	}
}

