using UnityEngine;
using System.Collections;

public class KingMosquitoAnimationScript : MonoBehaviour {
	public Sprite flyingUpSprite, flyingDownSprite;
	public Sprite phase1SideToSide, phase1Attack, phase1InvunAttackSprite;
	public Sprite phaseShift1Sprite, phaseShift2Sprite;
	public Sprite landedSprite;
	public Sprite suckingSprite;
	public Sprite invunSprite;
	public Sprite deadSprite;

	public Sprite death2, death3, death4, death5,
		death6, death7, death8, death9;

	private animations currentState;
	private SpriteRenderer spriteRenderer;

	public enum animations {
		flyingUp, flyingDown,
		phase1SideToSide, phase1Attacking, phase1Invun,
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

	public void changeStateToPhase1Flying () {
		spriteRenderer.sprite = phase1SideToSide;
		currentState = animations.phase1SideToSide;
	}

	public void changeStateToPhase1Attacking () {
		spriteRenderer.sprite = phase1Attack;
		currentState = animations.phase1Attacking;
	}

	public void changeStateToFlyingInvun () {
		spriteRenderer.sprite = phase1InvunAttackSprite;
		currentState = animations.phase1Invun;
	}

	public void changeStateToInvun () {
		spriteRenderer.sprite = invunSprite;
		currentState = animations.invun;
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

	public void changeStateToPhaseShift1 () {
		spriteRenderer.sprite = phaseShift1Sprite;
		currentState = animations.phaseShift1;
	}

	public void changeStateToPhaseShift2 () {
		spriteRenderer.sprite = phaseShift2Sprite;
		currentState = animations.phaseShift2;
	}


	public void explodeDeathAnimation () {
		//make 8 transforms and make some spin and move crazy
		  // and put the sprites on them

		Transform sucker = (new GameObject()).transform;
		sucker.position = transform.position;
		sucker.localScale = transform.localScale;
		sucker.parent = transform.parent;
		sucker.gameObject.AddComponent<SpinnerScript>().StartSpin(death9, -50f, new Vector2(10f, -10f), false);
		
		Transform rightleg = (new GameObject()).transform;
		rightleg.position = transform.position;
		rightleg.localScale = transform.localScale;
		rightleg.parent = transform.parent;
		rightleg.gameObject.AddComponent<SpinnerScript>().StartSpin(death8, -30f, new Vector2(10f, 0f), false);
		
		Transform leftleg = (new GameObject()).transform;
		leftleg.position = transform.position;
		leftleg.localScale = transform.localScale;
		leftleg.parent = transform.parent;
		leftleg.gameObject.AddComponent<SpinnerScript>().StartSpin(death7, 30f, new Vector2(-10f, 0f), false);

		Transform rightwing = (new GameObject()).transform;
		rightwing.position = transform.position;
		rightwing.localScale = transform.localScale;
		rightwing.parent = transform.parent;
		rightwing.gameObject.AddComponent<SpinnerScript>().StartSpin(death6, -30f, new Vector2(10f, 10f), false);
		
		Transform leftwing = (new GameObject()).transform;
		leftwing.position = transform.position;
		leftwing.localScale = transform.localScale;
		leftwing.parent = transform.parent;
		leftwing.gameObject.AddComponent<SpinnerScript>().StartSpin(death5, 30f, new Vector2(-10f, 10f), false);
		
		Transform eyes = (new GameObject()).transform;
		eyes.position = transform.position;
		eyes.localScale = transform.localScale;
		eyes.parent = transform.parent;
		eyes.gameObject.AddComponent<SpinnerScript>().StartSpin(death4, 50f, new Vector2(0f, 10f), false);
		
		Transform crown = (new GameObject()).transform;
		crown.position = transform.position;
		crown.localScale = transform.localScale;
		crown.parent = transform.parent;
		crown.gameObject.AddComponent<SpinnerScript>().StartSpin(death3, 180f, new Vector2(0f, -1f), true);

		Transform eyebrows = (new GameObject()).transform;
		eyebrows.position = transform.position;
		eyebrows.localScale = transform.localScale;
		eyebrows.parent = transform.parent;
		eyebrows.gameObject.AddComponent<SpinnerScript>().StartSpin(death2, 0f, new Vector2(0f, 10f), false);
	}

	public animations getState () {
		return currentState;
	}
}
