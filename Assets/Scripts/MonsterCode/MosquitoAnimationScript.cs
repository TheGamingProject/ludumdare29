using UnityEngine;
using System.Collections;

public class MosquitoAnimationScript : MonoBehaviour {
	public Sprite flyingSprite;
	public Sprite landedSprite;
	public Sprite suckingSprite;
	public Sprite invunSprite;
	public Sprite deadSprite;

	public Sprite vikingLandedSprite, vikingFlyingSprite, vikingSuckingSprite;

	private animations currentState;
	private SpriteRenderer spriteRenderer;

	public float vikingPercent = 5.0f;
	private bool isViking;

	public enum animations {
		landed, sucking, flying, invun, dead
	};

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start () {
		//changeStateToFlying();

		if (Random.Range(0f,1.0f) <= vikingPercent / 100.0f) 
			isViking = true;
	}

	void Update () {
	
	}
	
	public void changeStateToFlying () {
		if (isViking && vikingFlyingSprite != null) 
			spriteRenderer.sprite = vikingFlyingSprite;
		else
			spriteRenderer.sprite = flyingSprite;

		currentState = animations.flying;
	}

	public void changeStateToInvun () {
		if (invunSprite) 
			spriteRenderer.sprite = invunSprite;
	}

	public void changeStateToLanded () {
		if (isViking && vikingLandedSprite != null) 
			spriteRenderer.sprite = vikingLandedSprite;
		else
			spriteRenderer.sprite = landedSprite;

		currentState = animations.landed;
	}

	public void changeStateToSucking () {
		if (isViking && vikingSuckingSprite != null) 
			spriteRenderer.sprite = vikingSuckingSprite;
		else
			spriteRenderer.sprite = suckingSprite;

		currentState = animations.sucking;
	}

	public void changeStateToDead () {
		spriteRenderer.sprite = deadSprite;
		currentState = animations.dead;
	}

	public animations getState () {
		return currentState;
	}
}
