using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour {
	public float attackRate = .25f; // also how long the box collider stays up
	public Vector2 speed = new Vector2(50, 0);

	private Vector2 movement;
	private float attackCooldown;

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
			Debug.Log("attack");
			boxCollider.center = new Vector3(0.0f, .25f, 0.0f);
			attackCooldown = attackRate;
		} 
		if (attackCooldown < 0) {
			boxCollider.center = new Vector3(0.0f, .0f, 0.0f);
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
		Debug.Log("hey");
	/*	bool damagePlayer = false;
		
		// Collision with enemy
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
		if (enemy != null)
		{
			// Kill the enemy
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);
			
			damagePlayer = true;
		}
		
		// Damage the player
		if (damagePlayer)
		{
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if (playerHealth != null) playerHealth.Damage(1);
		}*/
	}

	public bool CanAttack {
		get{
			return attackCooldown <= 0f;
		}
	}
}