using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Vector3 velocity;
	private bool rotated;
	float timer;

	private Vector3 mousePos;
	public GameObject zombini;
	private bool flipped;

	// Use this for initialization
	void Start () {
		zombini = GameObject.Find("Zombini");
		flipped = false;


		rotated = true;
		timer = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;

		if (timer <= 0.0f)
				GameObject.Destroy (this.gameObject);

		float mag = Vector3.Magnitude(velocity);

		if(mag < 1.0f )
		{
			velocity.z = 0;
			velocity.Normalize();
			velocity.z = 1;
			velocity += velocity;
		}

		transform.Translate(velocity*2);

		if(rotated)
			rotate();


	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name.Equals("Crate(Clone)"))
		{
			GameObject.Destroy(coll.gameObject);
			GameObject.Destroy(this.gameObject);
		}

		if (coll.gameObject.name.Equals("Ground"))
		{
			GameObject.Destroy(this.gameObject);
		}

		if (coll.gameObject.layer.Equals(12) || coll.gameObject.layer.Equals(10))
		{
			GameObject.Destroy(this.gameObject);
		}
	}


	void rotate()
	{
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (transform.position.x < mousePos.x)
		{
			Vector3 flip = transform.localScale;
			flip.x *= -1;
			transform.localScale = flip;
		}
		

		if(mousePos.x > transform.position.x)
			transform.eulerAngles = 
				new Vector3(0,0,Mathf.Atan2((mousePos.y - transform.position.y),
				                            (mousePos.x - transform.position.x))*Mathf.Rad2Deg);
		else
			transform.eulerAngles = 
				new Vector3(0,0,Mathf.Atan2((mousePos.y - transform.position.y),
				                            (mousePos.x - transform.position.x))*Mathf.Rad2Deg + 180);
		
		
		
		rotated = false;
	}



}
