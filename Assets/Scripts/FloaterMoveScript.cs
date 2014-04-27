using UnityEngine;
using System.Collections;

public class FloaterMoveScript : MonoBehaviour {
	public Vector2 speedRange = new Vector2(5, 15);
	public Vector2 rotationSpeedRange = new Vector2(30, 60);
	
	public Sprite floater1Sprite;
	public Sprite floater2Sprite;
	public Sprite floater3Sprite;
	public Sprite floater4Sprite;
	public Sprite floater5Sprite;
	
	private float timeoutDeath = 30.0f;
	
	private float deathCooldown;

	public float speed;
	public float rotateSpeed;
	public int leftOrRight;
	
	// Use this for initialization
	void Start () {
		deathCooldown = timeoutDeath;
		int type = Random.Range(0, 2) + 1;
		
		switch (type) {
		case 1:
			GetComponentInChildren<SpriteRenderer>().sprite = floater1Sprite;
			break;
		case 2:
			GetComponentInChildren<SpriteRenderer>().sprite = floater2Sprite;
			break;
		case 3:
			GetComponentInChildren<SpriteRenderer>().sprite = floater3Sprite;
			break;
		case 4:
			GetComponentInChildren<SpriteRenderer>().sprite = floater4Sprite;
			break;
		case 5:
			GetComponentInChildren<SpriteRenderer>().sprite = floater5Sprite;
			break;
		}

		speed = Random.Range(speedRange.x,speedRange.y);
		rotateSpeed = Random.Range(rotationSpeedRange.x,rotationSpeedRange.y);

		speed = leftOrRight == 1 ? speed * -1 : speed;
		rotateSpeed = leftOrRight == 1 ? rotateSpeed * -1 : rotateSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (deathCooldown > 0) {
			deathCooldown -= Time.deltaTime;
		} else {
			Destroy(gameObject);
		}

		transform.Translate(new Vector3(Time.deltaTime * speed,0.0f,0.0f));
		GetComponentInChildren<SpriteRenderer>().transform.Rotate(new Vector3(0.0f,0.0f,Time.deltaTime * rotateSpeed));
	}
	
}
