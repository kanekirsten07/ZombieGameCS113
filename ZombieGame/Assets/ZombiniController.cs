using UnityEngine;
using System.Collections;


public class ZombiniController : MonoBehaviour {

	public float maxSpeed = 10f;
	private bool facingRight = true;
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public bool jump = false;				// Condition for whether the player should jump.
	Animator anim;
	private bool canShoot = true;
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	public GameObject bullet;

	void Start ()
	{
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
	}

	void Update ()
	{

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Grounded", grounded);
		if(Input.GetButtonDown("Jump")&& grounded)
		{
			jump = true;
		}

		if (Input.GetMouseButtonDown (0) && canShoot) 
			fire ();

	}

	void FixedUpdate()
	{
		float move = Input.GetAxis("Horizontal");
		//Debug.Log(move);
		anim.SetFloat("Speed", Mathf.Abs(move));
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
		//Debug.Log(transform.position);
		// If the input is moving the player right and the player is facing left...
		if(move > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(move< 0 && facingRight)
			// ... flip the player.
			Flip();

		if(jump)
		{
			// Play a random jump audio clip.
			//int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}else{

		}
	}

	void fire()
	{
		
		Vector3 spawnLocation = this.transform.position;

		float spawnDistance = 2.0f; 

		bullet = (GameObject)Instantiate(Resources.Load("prefab_bullet"), spawnLocation + (2*Vector3.right), transform.rotation);
		
		bullet.GetComponent<Bullet>().velocity = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - new Vector3(this.transform.position.x, this.transform.position.y, 0)).normalized;
		
		Debug.Log ("Camera.main.ScreenToWorldPoint (Input.mousePosition)= "+Camera.main.ScreenToWorldPoint (Input.mousePosition));
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
