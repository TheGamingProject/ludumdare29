using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public float totalHP = 100;
	public float regenRate = 1.0f;
	public float regenAmount = 2.5f;

	private int hpBarScaleMax = 16;
	private int hpBarScaleMaxAmount = 400;
	private int hpBarScaleStart = 3;
	private int hpBarAmountStart = 100;

	private float currentHp;
	private float regenCooldown;

	private bool dead = false;

	void Start() {
		currentHp = totalHP;
	}

	void Update () {
		if (regenCooldown > 0) {
			regenCooldown -= Time.deltaTime;
		}
		
		if (!dead && CanRegen) {
			//Debug.Log ("regen " + regenAmount);
			regenCooldown = regenRate;
			currentHp += regenAmount;
		}

		GameObject.Find("HealthText").GetComponent<GUIText>().text = "Health: " + currentHp;
		float hpBarScale = currentHp * .04f;
		Transform hpBarTransform = GameObject.Find("HpBar").transform;
		hpBarTransform.localScale = new Vector3(hpBarScale, hpBarTransform.localScale.y, hpBarTransform.localScale.z);

	}

	public void DamageBody(int amount) {
		if (dead) return;

		//Debug.Log ("Body Damaged: " + amount);
		currentHp -= amount;

		if (currentHp <= 0) {
			dead = true;
			transform.gameObject.AddComponent<GameOverScript>();
		}	
	}

	public bool CanRegen {
		get{
			return regenCooldown <= 0f;
		}
	}

}
