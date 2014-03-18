using UnityEngine;
using System.Collections;



public class CopScript : MonoBehaviour {

	public float moveSpeed = 2.5f;		// The speed the enemy moves at.
	public float HP = 4f;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public bool jump = false;				// Condition for whether the player should jump.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
	public float vomitSpeed = 1.0f;
	public GameObject vomitPrefab;
	private Transform vomitSpawn;
	private double secstoWait = 10.0;
	private double jumpWait = 20.0;
	private double groanWait = 25.0;
	bool grounded = false;
	public LayerMask whatIsGround;
	float groundRadius = 0.2f;
	private double timer;
	private double timer2;
	private double timer3;
	public bool canShoot = true;
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	public bool isTiming = false;
	public bool isTiming2 = false;
	public bool isTiming3 = false;
	public float damageAmount = 0f;
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
	private bool canJump = true;
	public AudioClip zombieGroan;
	private GameLoop mainLoop;
	public GameInfoScript gis;
	public Vector3 lastPos;

	public bool facingRight ;
	private float move = -1f;
	void Start()
	{
		vomitSpawn = transform.Find("vomitSpawn");
		groundCheck = transform.Find("groundCheckCop");
		mainLoop = (GameLoop) FindObjectOfType(typeof(GameLoop));
		//AudioSource.PlayClipAtPoint(zombieGroan, transform.position);
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
	}


	void beginTimer()
	{
		timer = 0;
		isTiming = true;
	}

	void beginTimer2()
	{
		timer2 = 0;
		isTiming2 = true;
	}

	void beginTimer3()
	{
		timer3 = 0;
		isTiming3 = true;
	}

	void Update()
	{
		if(isTiming)
		{
			timer += Time.deltaTime;
			timer2 += Time.deltaTime;
			timer3 += Time.deltaTime;
		}
		if(timer > secstoWait)
		{

			canShoot = true;
		}
		if(timer2 > jumpWait)
		{
			beginTimer2();
			canJump = true;
		}

		if(timer3 > groanWait)
		{
			beginTimer3();
		//	AudioSource.PlayClipAtPoint(zombieGroan, transform.position);
		}

	}

	void endTimer()
	{
		isTiming = false;
	}
	void FixedUpdate ()
	{

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		if(canJump && grounded)
		{
			jump = true;
			Debug.Log (jump);
		}


		if (!gis.chronoStopActive)
		{
			GetComponent<Animator>().enabled = true;
			// Set the enemy's velocity to moveSpeed in the x direction.
			walk();
			lastPos = transform.position;
		}
		
		else
		{
			transform.position = lastPos;
			GetComponent<Animator>().enabled = false;
		}



		// If the enemy has one hit point left and has a damagedEnemy sprite...
		if(HP == 1f && damagedEnemy != null)
			// ... set the sprite renderer's sprite to be the damagedEnemy sprite.
			ren.sprite = damagedEnemy;
		
		// If the enemy has zero or fewer hit points and isn't dead yet...
		if(HP <= 0f && !dead)
			// ... call the death function.
			Death ();

		if(jump && !gis.chronoStopActive)
		{
			// Play a random jump audio clip.
			//int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
			
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
			canJump = false;
		}
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
			Hurt (1f);
			//Debug.Log("Boop2");
			//Debug.Log(HP);
			AudioSource.PlayClipAtPoint(zombieGroan, transform.position);
		}

		else if(col.gameObject.tag == "missile" || col.gameObject.tag == "missileExplosion")
		{
			Hurt (2f);
			AudioSource.PlayClipAtPoint(zombieGroan, transform.position);
		}

		else if(col.gameObject.tag == "energy")
		{
			Hurt (0.01f);
			AudioSource.PlayClipAtPoint(zombieGroan, transform.position);
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
	
	public void Hurt(float damageAmount)
	{
		if (gis.overpowerActive)
			Death();
		else
		{
			// Reduce the number of hit points by "damageAmount".
			HP-=damageAmount;
		}
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
