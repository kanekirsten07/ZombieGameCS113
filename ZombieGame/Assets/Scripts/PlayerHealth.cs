using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
	public float health = 100f;					// The player's health.
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	public float damageAmount ;			// The amount of damage to take when enemies touch the player
	
	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	
	private ZombiniController ZombiniControl;
	private Animator anim;						// Reference to the Animator on the player
	public GameInfoScript gis;
	public AudioClip hurtGroan;
	
	void Awake ()
	{
		// Setting up references.
		ZombiniControl = GetComponent<ZombiniController>();
		healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		
		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;

		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
	}
	
	
	void OnCollisionEnter2D (Collision2D col)
	{
		damageAmount = 0f;
		//Debug.Log("Boop");
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Walker")
		{
			
			damageAmount = 1f;
			Debug.Log("Walker");
			AudioSource.PlayClipAtPoint(hurtGroan, transform.position);
		}else if(col.gameObject.tag == "Nurse")
		{
			Debug.Log("Nurse");
			damageAmount = 5f;
			AudioSource.PlayClipAtPoint(hurtGroan, transform.position);

		}else if(col.gameObject.tag == "Cop")
		{
			Debug.Log("Cop");
			damageAmount = 5f;
			AudioSource.PlayClipAtPoint(hurtGroan, transform.position);

		}else if(col.gameObject.tag == "Flood")
		{
			Debug.Log("Flood");
			damageAmount = 5f;
			AudioSource.PlayClipAtPoint(hurtGroan, transform.position);

		}else if(col.gameObject.tag == "Minion")
		{
			Debug.Log("Minion");
			damageAmount = 2f;
			AudioSource.PlayClipAtPoint(hurtGroan, transform.position);

		}else if(col.gameObject.tag == "Vomit")
		{
			Debug.Log("Vomit");
			damageAmount = 1f;
			AudioSource.PlayClipAtPoint(hurtGroan, transform.position);

		}
		handleCollisionStuffs(col);
		
	}
	
	
	void handleCollisionStuffs(Collision2D col)
	{
		/*if (Time.time > lastHitTime + repeatDamagePeriod) 
		{
		*/
		// ... and if the player still has health...
		if(health > 0f)
		{
			// ... take damage and reset the lastHitTime.
			TakeDamage(col.transform); 
			lastHitTime = Time.time; 
		}
		// If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
		else
		{
			// Find all of the colliders on the gameobject and set them all to be triggers.
			Collider2D[] cols = GetComponents<Collider2D>();
			foreach(Collider2D c in cols)
			{
				c.isTrigger = true;
			}
			
			// Move all sprite parts of the player to the front
			SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
			foreach(SpriteRenderer s in spr)
			{
				s.sortingLayerName = "UI";
			}

			

		Application.LoadLevel(1);

		}
		//}
	}

	public void takeExplosionDamage(int explosionDamage)
	{
		health -= explosionDamage;
		UpdateHealthBar();
		Debug.Log("explody");
	}
	
	void TakeDamage (Transform enemy)
	{
		if (!gis.repulsorActive)
		{
			// Make sure the player can't jump.
			ZombiniControl.jump = false;
			
			// Create a vector that's from the enemy to the player with an upwards boost.
			Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;
			
			// Add a force to the player in the direction of the vector and multiply by the hurtForce.
			//rigidbody2D.AddForce(hurtVector * hurtForce);
			
			// Reduce the player's health by 10.
			health -= damageAmount;
			Debug.Log(health);
			
			// Update what the health bar looks like.
			UpdateHealthBar();
			
			// Play a random clip of the player getting hurt.
			//int i = Random.Range (0, ouchClips.Length);
			//AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
		}
	}
	
	
	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
		
		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}
}