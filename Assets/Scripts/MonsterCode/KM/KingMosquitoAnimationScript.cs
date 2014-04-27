using UnityEngine;
using System.Collections;

public class KingMosquitoAnimationScript : MonoBehaviour {
	public Sprite flyingUpSprite, flyingDownSprite;
	public Sprite phase1SideToSide, phase1Attack;
	public Sprite phaseShift1Sprite, phaseShift2Sprite;
	public Sprite landedSprite;
	public Sprite suckingSprite;
	public Sprite invunSprite;
	public Sprite deadSprite;

	private animations currentState;
	private SpriteRenderer spriteRenderer;

	public enum animations {
		flying, flyingUp, flyingDown,
		phase1SideToSide,
		landed, sucking, 
		phaseShift1, phaseShift2,
		invun, dead
	};

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start () {
		//changeStateToFlying();
	}

	void Update () {
	
	}
		
	public void changeStateToFlyingUp () {
		spriteRenderer.sprite = flyingUpSprite;
		currentState = animations.flyingUp;
	}
	
	public void changeStateToFlyingDown () {
		spriteRenderer.sprite = flyingDownSprite;
		currentState = animations.flyingDown;
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
