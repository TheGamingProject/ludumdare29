using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public float totalHP = 100;
	public float regenRate = 1.0f;
	public float regenAmount = 2.5f;

	private float currentHp;
	private float regenCooldown;

	void Start() {
		currentHp = totalHP;
	}

	void Update () {
		if (regenCooldown > 0) {
			regenCooldown -= Time.deltaTime;
		}
		
		if (CanRegen) {
			Debug.Log ("regen " + regenAmount);
			regenCooldown = regenRate;
			currentHp += regenAmount;
		}

		GameObject.Find("HealthText").GetComponent<GUIText>().text = "Health: " + currentHp;
	}

	public void DamageBody(int amount) {
		Debug.Log ("Body Damaged: " + amount);
		currentHp -= amount;

		if (currentHp <= 0) {
			GameObject.Find("StatusText").GetComponent<GUIText>().text = "GAME OVER";
			Debug.Log("GAME OVER");
		}	
	}

	public bool CanRegen {
		get{
			return regenCooldown <= 0f;
		}
	}

}
