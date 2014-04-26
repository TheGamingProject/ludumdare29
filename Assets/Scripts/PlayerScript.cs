using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour {
	public float attackDamage = 1.0f;
	public float attackRate = .25f; // also how long the box collider stays up
	public Vector2 speed = new Vector2(50, 0);
	
	public Vector2 upSize = new Vector2(.6f, .725f);
	public Vector2 upCenter = new Vector2(0.0f, .62f);
	public Vector2 downSize = new Vector2(.6f, .4f);
	public Vector2 downCenter = new Vector2(0.0f, .3f);


	private Vector2 movement;
	private float attackCooldown;

	public Sprite upSprite;
	public Sprite downSprite;

	void Start() {
		attackCooldown = 0f;
	}

	void Update() {
		// cooldown
		if (attackCooldown > 0) {
			attackCooldown -= Time.deltaTime;
		}

		float inputX = Input.GetAxis("Horizontal");

		movement = new Vector2(speed.x * inputX, 0);

		bool shoot = Input.GetButtonDown("Fire1");

		BoxCollider2D boxCollider = GetComponent("BoxCollider2D") as BoxCollider2D;

		if (shoot && CanAttack) {
			boxCollider.size = new Vector3(upSize.x, upSize.y, 0.0f);
			boxCollider.center = new Vector3(upCenter.x, upCenter.y, 0.0f);
			attackCooldown = attackRate;
			GetComponent<SpriteRenderer>().sprite = upSprite;
		}
		if (attackCooldown < 0) {
			boxCollider.size = new Vector3(downSize.x, downSize.y, 0.0f);
			boxCollider.center = new Vector3(downCenter.x, downCenter.y, 0.0f);
			GetComponent<SpriteRenderer>().sprite = downSprite;
		}
	}
	
	void FixedUpdate() {
		rigidbody2D.velocity = movement;

		// from http://answers.unity3d.com/questions/509283/limit-a-sprite-to-not-go-off-screen.html 

		Vector3 playerSize = renderer.bounds.size;
		
		// Here is the definition of the boundary in world point
		var distance = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x + (playerSize.x/2);
		var rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance)).x - (playerSize.x/2);

		// Here the position of the player is clamped into the boundaries
		transform.position = (new Vector3 (
			Mathf.Clamp (transform.position.x, leftBorder, rightBorder),
			transform.position.y, transform.position.z)
		                      );
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log("hit");
		
		// Collision with enemy
		EnemyScript enemy = collider.gameObject.GetComponent<EnemyScript>();
		if (enemy != null){
			// Kill the enemy
			EnemyHealthScript enemyHealth = enemy.GetComponent<EnemyHealthScript>();
			if (enemyHealth != null) enemyHealth.Damage(attackDamage);

		}

	}

	public bool CanAttack {
		get{
			return attackCooldown <= 0f;
		}
	}
}