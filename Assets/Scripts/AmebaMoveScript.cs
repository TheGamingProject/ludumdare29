using UnityEngine;
using System.Collections;

public class AmebaMoveScript : MonoBehaviour {
	public float speed = 10.0f;
	public int type;

	public Sprite powerup1Sprite;
	public Sprite powerup2Sprite;

	private float timeoutDeath = 5.0f;

	private float deathCooldown;
	private AudioSource deathNoise; 

	// Use this for initialization
	void Start () {
		deathCooldown = timeoutDeath;
		type = Random.Range(0, 2) + 1;



		switch (type) {
		case 1: // health
			GetComponent<SpriteRenderer>().sprite = powerup1Sprite;
			break;
		case 2: // size
			GetComponent<SpriteRenderer>().sprite = powerup2Sprite;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (deathCooldown > 0) {
			deathCooldown -= Time.deltaTime;
		} else {
			Destroy(gameObject);
		}

		transform.Translate(new Vector3(Time.deltaTime * speed,0.0f,0.0f));
	}

}
