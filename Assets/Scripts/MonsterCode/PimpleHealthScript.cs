using UnityEngine;
using System.Collections;

public class PimpleHealthScript : MonoBehaviour {
	public float totalHP = 2;
	public float invulnTime = .75f; //seconds
	public int pointsAwarded = 2;

	public Sprite normalSprite;
	public Sprite invulnSprite;

	private float currentHp;
	private float invlunCooldown;

	void Start() {
		currentHp = totalHP;
		GetComponent<EnemyScript>().StartAttacking();
	}
	
	void Update() {
		if (invlunCooldown > 0) {
			invlunCooldown -= Time.deltaTime;
		} else {
			GetComponent<SpriteRenderer>().sprite = normalSprite;
		}
	}
	
	public void Damage(float amount) {
		if (!isNotInvlun) return;
		Debug.Log("Hit Pimple");

		invlunCooldown = invulnTime;
		GetComponent<SpriteRenderer>().sprite = invulnSprite;
		
		currentHp -= amount;
		
		if (currentHp <= 0) {
			Debug.Log("KILLED enemy");
			Destroy(gameObject);
			GameObject.Find("Scripts").BroadcastMessage("GivePoints", pointsAwarded);
		}	
		
	}
	//
	void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log("enemy hey");
	}
	
	bool isNotInvlun {
		get{
			return invlunCooldown <= 0f;
		}
	}
}
