using UnityEngine;
using System.Collections;

public class NurseScript : MonoBehaviour {
	
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public float HP = 2f;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
	private double jumpWait = 20.0;
	bool grounded = false;
	public LayerMask whatIsGround;
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	float groundRadius = 0.2f;
	private double timer;
	public bool isTiming = false;
	private bool canJump = true;
	public bool jump = false;				// Condition for whether the player should jump.
	public float damageAmount = 0f;
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	public bool facingRight ;
	private bool dead = false;			// Whether or not the enemy is dead.
	private GameLoop mainLoop;
	private float move = -1f;

	public GameInfoScript gis;

	void Start()
	{
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
		mainLoop = (GameLoop) FindObjectOfType(typeof(GameLoop));
		groundCheck = transform.Find("groundCheckNurse");
		beginTimer();
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
		if(timer > jumpWait)
		{
			beginTimer();
			canJump = true;
		}
		
	}

	void endTimer()
	{
		isTiming = false;
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		
		//Debug.Log("boop");
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "pistol_bullet")
		{
			Hurt (1);
		//	Debug.Log("Boop1");
		}

		else if(col.gameObject.tag == "missile")
		{
			Hurt (2f);
		}
		
		else if(col.gameObject.tag == "energy")
		{
			Hurt (0.01f);
		}
		//handleCollisionStuffs(col);
		
	}
	
	void FixedUpdate ()
	{
	
<<<<<<< HEAD
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		Debug.Log (jump);
		if(canJump && grounded)
		{
			jump = true;
			Debug.Log (jump);
		}
		// Set the enemy's velocity to moveSpeed in the x direction.
		walk();
=======
		
		if (!gis.chronoStopActive)
		{
			// Set the enemy's velocity to moveSpeed in the x direction.
			walk();
		}
>>>>>>> FETCH_HEAD
		
		// If the enemy has one hit point left and has a damagedEnemy sprite...
		if(HP == 1 && damagedEnemy != null)
			// ... set the sprite renderer's sprite to be the damagedEnemy sprite.
			ren.sprite = damagedEnemy;
		
		// If the enemy has zero or fewer hit points and isn't dead yet...
		if(HP <= 0 && !dead)
			// ... call the death function.
			Death ();

		if(jump)
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

	void walk()
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

	}
	
	void Death()
	{
		// Find all of the sprite renderers on this object and it's children.
		GameObject.Destroy(this.gameObject);
		
		// Increase the score by 100 points
		mainLoop.totalScore += 20;
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

	