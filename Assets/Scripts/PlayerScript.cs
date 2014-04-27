using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour {
	public float attackDamage = 1.0f;
	public float attackRate = .4f; // also how long the box collider stays up
	public float stayUpRate = .2f;
	public Vector2 speed = new Vector2(50, 0);
	
	public Vector2 upSize = new Vector2(.6f, .725f);
	public Vector2 upCenter = new Vector2(0.0f, .62f);
	public Vector2 midSize = new Vector2(.6f, .725f);
	public Vector2 midCenter = new Vector2(0.0f, .45f);
	public Vector2 downSize = new Vector2(.6f, .4f);
	public Vector2 downCenter = new Vector2(0.0f, .3f);

	// power ups
	public int amebaHealAmount = 15;
	public float widthSizeScale = 1.25f;
	public float sizePowerUpTimeLength = 5.0f;

	private Vector2 movement;
	private float attackCooldown;
	private float stayUpCooldown;
	private float stayMidCooldown;

	public Sprite upSprite;
	public Sprite midSprite;
	public Sprite downSprite;

	private bool alive = true;

	private float sizePowerupCooldown; 
	private bool big = false;

	private float stayMidRate = .05f;

	float moveThreshold = .2f;
	
	private float movex;

	private SpriteRenderer spriteRenderer;
	void Start() {
		attackCooldown = 0f;
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() {
		if (!alive) return;

		// cooldown
		if (attackCooldown > 0) {
			attackCooldown -= Time.deltaTime;
			stayUpCooldown -= Time.deltaTime;
			stayMidCooldown -= Time.deltaTime;
		}

		if (sizePowerupCooldown > 0) {
			sizePowerupCooldown -= Time.deltaTime;
		}

		// movement
		float inputX = Input.GetAxis("Horizontal");
	
			// for android?
		float iPx = Input.acceleration.x;
		if (Mathf.Abs(iPx) > moveThreshold) {
			inputX = Mathf.Sign(iPx);
		} 
		movement = new Vector2(speed.x * inputX, 0);

		// attacking
		bool shoot = Input.GetButtonDown("Fire1");

		BoxCollider2D boxCollider = GetComponent("BoxCollider2D") as BoxCollider2D;

		if (shoot && CanAttack) {
			attackCooldown = attackRate;
			stayUpCooldown = stayUpRate;
			stayMidCooldown = stayMidRate;
		}

		if (stayMidCooldown > 0) {
			boxCollider.size = new Vector3(midSize.x, midSize.y, 0.0f);
			boxCollider.center = new Vector3(midCenter.x, midCenter.y, 0.0f);
			spriteRenderer.sprite = midSprite;
		} else {
			boxCollider.size = new Vector3(upSize.x, upSize.y, 0.0f);
			boxCollider.center = new Vector3(upCenter.x, upCenter.y, 0.0f);
			spriteRenderer.sprite = upSprite;
		}
		if (stayUpCooldown <= 0) {
			boxCollider.size = new Vector3(downSize.x, downSize.y, 0.0f);
			boxCollider.center = new Vector3(downCenter.x, downCenter.y, 0.0f);
			spriteRenderer.sprite = downSprite;
		} 

		if (big && sizePowerupCooldown < 0) {
			gameObject.transform.localScale = new Vector3( 2, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
			big = false;
		}
	}
	
	void FixedUpdate() {
		rigidbody2D.velocity = movement;

		// from http://answers.unity3d.com/questions/509283/limit-a-sprite-to-not-go-off-screen.html 

		Vector3 playerSize = (GetComponent<BoxCollider2D>()).size;
		//Debug.Log(renderer.bounds);
		//Debug.Log(playerSize);

		//playerSize = renderer.bounds.size;
		
		// Here is the definition of the boundary in world point
		var distance = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x + (playerSize.x/2);
		var rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance)).x - (playerSize.x/2);

		// Here the position of the player is clamped into the boundaries
		transform.position = (new Vector3 (
			Mathf.Clamp (transform.position.x, leftBorder - .2f, rightBorder + .2f),
			transform.position.y, transform.position.z)
		                      );
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		//Debug.Log("hit");
		
		// Collision with enemy
		EnemyScript enemy = collider.gameObject.GetComponent<EnemyScript>();
		if (enemy != null){
			// Kill the mosquito enemy
			MosquitoHealthScript enemyHealth = enemy.GetComponent<MosquitoHealthScript>();
			if (enemyHealth != null) enemyHealth.Damage(attackDamage);

		}

		//collide with ameba
		AmebaMoveScript ameba = collider.gameObject.GetComponent<AmebaMoveScript>();
		if (ameba != null){
			Debug.Log ("ameba");

			switch(ameba.type) {
			case 1:
				Debug.Log("Heal Powerup");
				GameObject.Find("Scripts").GetComponent<HealthScript>().DamageBody(-amebaHealAmount);
				break;
			case 2:
				Debug.Log("Double Width Powerup");
				float powerupWidth = gameObject.transform.localScale.x * widthSizeScale;
				if (powerupWidth > 15) powerupWidth = 15.0f;
				gameObject.transform.localScale = new Vector3(powerupWidth, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
				sizePowerupCooldown = sizePowerUpTimeLength;
				big = true;
				break;
			}
			
			Destroy(ameba.gameObject);
		}

	}

	public bool CanAttack {
		get{
			return attackCooldown <= 0f;
		}
	}

	public void Die () {
		alive = false;
		movement = new Vector2(0,0);
	}
}