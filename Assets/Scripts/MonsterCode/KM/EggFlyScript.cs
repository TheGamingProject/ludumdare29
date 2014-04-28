using UnityEngine;
using System.Collections;

public class EggFlyScript : MonoBehaviour{
	public float skinLevelHeight = -1.65f;
	public float flyingDownTime = .5f;

	public float flyToBossHeight = 3.75f;
	public float flyToBossTime = .5f;

	private Vector2 flightLength;

	private bool flying = true;
	private Vector2 movement;
	private BoxCollider2D boxCollider;

	void Start (){
		GetComponent<EggAnimationScript>().changeStateToDownFlying();

		boxCollider = (GetComponent("BoxCollider2D") as BoxCollider2D);
		boxCollider.enabled = false;

		movement = new Vector2(0.0f, (skinLevelHeight - transform.position.y) / flyingDownTime);

	}

	void FixedUpdate() {
		if (flying) {
			rigidbody2D.velocity = movement;

			if (transform.position.y < skinLevelHeight) {
				LandOnSkin();
			}

			if (transform.position.y > 10) {
				Destroy(gameObject);
			}
 		}
	}

	void LandOnSkin () {
		transform.position = new Vector3(transform.position.x, skinLevelHeight, transform.position.z);
		flying = false;
		rigidbody2D.velocity = new Vector2(0,0);
		GetComponent<EggAnimationScript>().changeStateToIdle();
		boxCollider.enabled = true;
		GetComponents<AudioSource>()[0].Play();
	}

	public void BlastOff () {
		flying = true;
		movement = new Vector2(0.0f, (flyToBossHeight - transform.position.y) / flyingDownTime);
		GetComponent<EggAnimationScript>().changeStateToUpFlying();
	}
}

