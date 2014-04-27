using UnityEngine;
using System.Collections;

public class MosquitoHealthScript : MonoBehaviour {
	public float totalHP = 1;
	public float invulnTime = .5f; //seconds
	public int pointsAwarded = 1;

	public Sprite deathSprite;
	public float deathTime = .5f;
	
	public Sprite invulnSprite;

	private Sprite normalSprite;
	
	private float currentHp;
	private float invlunCooldown;

	private bool death = false;
	private float deathCooldown;
	
	void Start() {
		currentHp = totalHP;
	}

	void Update() {
		if (invlunCooldown > 0) {
			invlunCooldown -= Time.deltaTime;
		} else {
			if (normalSprite) GetComponent<SpriteRenderer>().sprite = normalSprite;
		}

		if (death && deathCooldown > 0) {
			deathCooldown -= Time.deltaTime;
		}

		if (death && deathCooldown <= 0) {
			
			Destroy(gameObject);
		}
	}

	public void Damage(float amount) {
		if (!isNotInvlun) return;

		currentHp -= amount;


		invlunCooldown = invulnTime;
		if (invulnSprite) {
			normalSprite = GetComponent<SpriteRenderer>().sprite;
			GetComponent<SpriteRenderer>().sprite = invulnSprite;
		}
		if (currentHp <= 0) {
			Debug.Log("KILLED enemy");
			GameObject.Find("Scripts").BroadcastMessage("GivePoints", pointsAwarded);

			death = true;
			GetComponent<SpriteRenderer>().sprite = deathSprite;
			deathCooldown = deathTime;
		}	
			
	}

	bool isNotInvlun {
		get{
			return invlunCooldown <= 0f;
		}
	}

}
