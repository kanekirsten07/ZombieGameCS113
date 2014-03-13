using UnityEngine;
using System.Collections;



public class CopScript : MonoBehaviour {

	public float moveSpeed = 2.5f;		// The speed the enemy moves at.
	public int HP = 4;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
	public float vomitSpeed = 1.0f;
	public GameObject vomitPrefab;
	private Transform vomitSpawn;
	private double secstoWait = 10.0;
	private double timer;
	public bool canShoot = true;
	public bool isTiming = false;
	public float damageAmount = 0f;
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.


	private GameLoop mainLoop;
	public GameInfoScript gis;

	public bool facingRight ;
	private float move = -1f;
	void Start()
	{
		vomitSpawn = transform.Find("vomitSpawn");
		mainLoop = (GameLoop) FindObjectOfType(typeof(GameLoop));

		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
	}


	void beginTimer()
	{
		timer = 0;
		isTiming = true;
	}

	void Update()
	{
		if(isTiming)
		{
			timer += Time.deltaTime;
		}
		if(timer > secstoWait)
		{

			canShoot = true;
		}

	}

	void endTimer()
	{
		isTiming = false;
	}
	void FixedUpdate ()
	{

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

	private void projectilevomit()
	{
		//Aka throw a projectile
		//Debug.Log(transform.position);
		if(canShoot)
		{

		
			//Debug.Log("Blech!");

			 float spawnDistance = 2.0f; // don't want the bullet spawn in centre
			
			// ...   
			if(facingRight)
				GameObject.Instantiate(vomitPrefab, this.transform.position + (4*Vector3.right), transform.rotation);

			if(!facingRight)
				GameObject.Instantiate(vomitPrefab, this.transform.position + (4*Vector3.left), transform.rotation);
			//Debug.Log(transform.position);
			

			this.canShoot = false;

			beginTimer();
		}

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
			//Debug.Log("Boop2");
			//Debug.Log(HP);
		}
		//handleCollisionStuffs(col);
		
	}

	private void walk()
	{
		GameObject go = GameObject.Find("Zombini");
		Transform target = go.transform;
		Vector2 walker = transform.position;
		Vector2 player = target.position;
		//Debug.Log("Walker position:" +walker);
		//Debug.Log("Player Position: " +player);
		
		
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


			projectilevomit();

	}
	
	public void Hurt(int damageAmount)
	{
		// Reduce the number of hit points by one.
		HP-=damageAmount;
	}
	void Death()
	{
		GameObject.Destroy(this.gameObject);
		
		// Increase the score by 100 points
		mainLoop.totalScore += 30;
		// Set dead to true.
		dead = true;
		
		
		// Play a random audioclip from the deathClips array.
		//int i = Random.Range(0, deathClips.Length);
		//AudioSource.PlayClipAtPoint(deathClips[i], transform.position);
	}
	
	
	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}
