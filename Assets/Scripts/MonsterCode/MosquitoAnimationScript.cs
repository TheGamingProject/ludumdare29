using UnityEngine;
using System.Collections;

public class MosquitoAnimationScript : MonoBehaviour {
	public Sprite flyingSprite;
	public Sprite landedSprite;
	public Sprite suckingSprite;
	public Sprite invunSprite;
	public Sprite deadSprite;

	private animations currentState;
	private SpriteRenderer spriteRenderer;

	public enum animations {
		landed, sucking, flying, invun, dead
	};

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start () {
		//changeStateToFlying();
	}

	void Update () {
	
	}
	
	public void changeStateToFlying () {
		spriteRenderer.sprite = flyingSprite;
		currentState = animations.flying;
	}

	public void changeStateToInvun () {
		if (invunSprite) 
			spriteRenderer.sprite = invunSprite;
	}

	public void changeStateToLanded () {
		spriteRenderer.sprite = landedSprite;
		currentState = animations.landed;
	}

	public void changeStateToSucking () {
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
