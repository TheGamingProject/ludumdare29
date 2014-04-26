using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public float totalHP = 100;

	private float currentHp;

	void Start() {
		currentHp = totalHP;
	}

	public void Damage(int amount) {
		currentHp -= amount;

		if (currentHp <= 0) {
			Debug.Log("GAME OVER");
		}	
	}

}
