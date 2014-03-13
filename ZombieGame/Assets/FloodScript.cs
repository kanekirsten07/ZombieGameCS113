﻿using UnityEngine;
using System.Collections;

public class FloodScript : MonoBehaviour {

	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public int HP = 2;					// How many times the enemy can be hit before it dies.

	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
	public float damageAmount = 0f;
	private bool dead = false;			// Whether or not the enemy is dead.
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	public GameObject Minion;
	public PlayerHealth health;
	private GameLoop mainLoop;
	public bool facingRight ;
	private float move = -1f;
	private int explosionDamage = 10;

	public GameInfoScript gis;

	void Start()
	{
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
		mainLoop = (GameLoop) FindObjectOfType(typeof(GameLoop));
		health = (PlayerHealth) FindObjectOfType(typeof(PlayerHealth));
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		
		//Debug.Log("boop");
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "pistol_bullet")
		{
			if (gis.overpowerActive)
				Death();
			else
				Hurt (1);
			//Debug.Log("Boop1");
		}
		if(col.gameObject.tag == "Zombini")
		{
			Explode();
		}
		//handleCollisionStuffs(col);
		
	}
	void FixedUpdate ()
	{
		
		// Set the enemy's velocity to moveSpeed in the x direction.
		walk();
		
		// If the enemy has one hit point left and has a damagedEnemy sprite...
		if(HP == 1 && damagedEnemy != null)
			// ... set the sprite renderer's sprite to be the damagedEnemy sprite.
			ren.sprite = damagedEnemy;
		
		// If the enemy has zero or fewer hit points and isn't dead yet...
		if(HP <= 0 && !dead)
			// ... call the death function.
			Death ();
	}
	
	public void Hurt(int damageAmount)
	{
		// Reduce the number of hit points by one.
		HP-=damageAmount;
	}
	void walk()
	{
		GameObject go = GameObject.Find("Zombini");
		Transform target = go.transform;
		Vector2 walker = transform.position;
		Vector2 player = target.position;
		//Debug.Log("Flood position:" +walker);
		//Debug.Log("Player Position: " +player);
		
		//Debug.Log(facingRight);
		if(walker[0] < player[0])
		{
			
			if(!facingRight)
			{
				
				Flip ();
				facingRight = true;
				move = 1f;
				
			}
			rigidbody2D.velocity = new Vector2(move * moveSpeed, rigidbody2D.velocity.y);
			
			
		}else
		{
			
			if(facingRight)
			{
				Flip ();
				facingRight = false;
				move=-1f;
				
			}
			rigidbody2D.velocity = new Vector2(move * moveSpeed, rigidbody2D.velocity.y);	
		}

		if(Mathf.Abs(Mathf.Abs(walker[0]) - Mathf.Abs(player[0])) <= 3)
		{
			//Debug.Log("Boom");
			Explode();
		}
		
	}
	
	void Death()
	{

		GameObject.Destroy(this.gameObject);
		
		
		// Increase the score by 100 points
		mainLoop.totalScore += 15;
		// Set dead to true.
		dead = true;
		
		
		// Play a random audioclip from the deathClips array.
		//int i = Random.Range(0, deathClips.Length);
		//AudioSource.PlayClipAtPoint(deathClips[i], transform.position);
	}

	void Explode()
	{
		// Find all of the sprite renderers on this object and it's children.
		if(!dead)
		{
			GameObject.Destroy(this.gameObject);

			health.takeExplosionDamage(explosionDamage);

			
		//Spawn minions

		for(int j= 0; j < 5; j++)
		{
				//Debug.Log("Spawn minion");
		GameObject.Instantiate(Minion, this.transform.position + (4*Vector3.right), transform.rotation);
		}
		}
	}
	
	
	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}
