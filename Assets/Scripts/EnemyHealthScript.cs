using UnityEngine;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour {
	public float totalHP = 1;
	public float invulnTime = .5f; //seconds
	
	private float currentHp;
	
	void Start() {
		currentHp = totalHP;
	}
	
	public void Damage(int amount) {
		currentHp -= amount;
		
		if (currentHp <= 0) {


			Debug.Log("KILLED enemy");
		}	
	}
	//
	void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log("enemy hey");
	}
}
