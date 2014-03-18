using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {
	
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public float HP = 3f;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip zombieGroan;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
	public bool facingRight ;
	public float damageAmount = 0f;
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
	private GameLoop mainLoop;
	private double jumpWait = 10.0;
	private double groanwait = 10.0;
	bool grounded = false;
	public LayerMask whatIsGround;
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	float groundRadius = 0.2f;
	private double timer;
	private double timer2;
	public bool isTiming = false;
	public bool isTiming2 = false;
	private bool canJump = true;
	public bool jump = false;		
	public GameInfoScript gis;
	private float move = -1f;
	public Vector3 lastPos;
	

	void Start()
	{
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
		facingRight = false;
		mainLoop = (GameLoop) FindObjectOfType(typeof(GameLoop));
		groundCheck = transform.Find("groundCheckWalker");
		beginTimer();
		//AudioSource.PlayClipAtPoint(zombieGroan, transform.position);
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
	
	void Update()
	{
		if(isTiming)
		{
			timer += Time.deltaTime;
			timer2 += Time.deltaTime;
		}
		if(timer > jumpWait)
		{
			beginTimer();
			canJump = true;
		}
		if(timer2 > groanwait)
		{
			beginTimer2();
			//AudioSource.PlayClipAtPoint(zombieGroan, transform.position);
		}		
	}
	
	void endTimer()
	{
		isTiming = false;
	}
	

	void FixedUpdate ()
	{


		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		//Debug.Log (jump);
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

		
		// If the enemy has zero or fewer hit points and isn't dead yet...
		if(HP <= 0 && !dead)
		{
			// ... call the death function.
			Death ();
		}

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

		//Debug.Log(walker[0]);
		//Debug.Log(player[0]);
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

	}

	void OnCollisionEnter2D (Collision2D col)
	{

		//Debug.Log("boop");
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "pistol_bullet")
		{
			Hurt (1);
			//	Debug.Log("Boop1");
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
	
	void Death()
	{
		GameObject.Destroy(this.gameObject);
		// Increase the score by 100 points
		mainLoop.totalScore += 10;
		// Set dead to true.
		dead = true;


		// Play a random audioclip from the deathClips array.
		//int i = Random.Range(0, deathClips.Length);
		//AudioSource.PlayClipAtPoint(deathClips[i], transform.position);
		

	}
	
	
	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
