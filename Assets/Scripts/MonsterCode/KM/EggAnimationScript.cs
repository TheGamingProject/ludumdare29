using UnityEngine;
using System.Collections;

public class EggAnimationScript : MonoBehaviour {
	public Sprite downSprite;
	public Sprite idleSprite;
	public Sprite upSprite;
	public Sprite hatch1Sprite;
	public Sprite hatch2Sprite;

	private SpriteRenderer spriteRenderer;

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void changeStateToDownFlying () {
		spriteRenderer.sprite = downSprite;
	}

	public void changeStateToUpFlying () {
		spriteRenderer.sprite = upSprite;
	}

	public void changeStateToIdle () {
		spriteRenderer.sprite = idleSprite;
	}

	public void changeStateToHatch1 () {
		spriteRenderer.sprite = hatch1Sprite;
	}

	public void changeStateToHatch2 () {
		spriteRenderer.sprite = hatch2Sprite;
	}
}
