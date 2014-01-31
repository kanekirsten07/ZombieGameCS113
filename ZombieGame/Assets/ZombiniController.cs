using UnityEngine;
using System.Collections;


public class ZombiniController : MonoBehaviour {

	public float maxSpeed = 10f;
	private bool facingRight = true;
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public bool jump = false;				// Condition for whether the player should jump.
	void Start ()
	{
	}

	void Update ()
	{
		if(Input.GetButtonDown("Jump"))
			jump = true;
	}

	void FixedUpdate()
	{
		float move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);


		if(jump)
		{
			// Set the Jump animator trigger parameter.
			//anim.SetTrigger("Jump");
			
			// Play a random jump audio clip.
			//int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
			
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
	}

}
