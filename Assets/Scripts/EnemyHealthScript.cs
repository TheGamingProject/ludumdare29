using UnityEngine;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour {
	public float totalHP = 1;
	public float invulnTime = .5f; //seconds
	public int pointsAwarded = 1;
	
	private float currentHp;
	
	void Start() {
		currentHp = totalHP;
	}
	
	public void Damage(float amount) {
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
}
