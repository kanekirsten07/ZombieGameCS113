using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public Vector3 velocity;
	private bool rotated;
	public float timer;

	private Vector3 mousePos;
	public GameObject zombini;
	private bool flipped;
	
	private Vector3 flip;


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
		{
			GameObject.Destroy (this.gameObject);
			// make the missile explode

		}

		float mag = Vector3.Magnitude(velocity);
		
		if(mag < 1.0f )
		{
			velocity.z = 0;
			velocity *= 2.0f;
			velocity.z = 1;
		}


		transform.Translate(velocity);

		//Debug.Log("===============================velocity = " + velocity/10);

		if(rotated)
			rotate();
	}





	void rotate()
	{
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		
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







	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name.Equals("Crate(Clone)"))
		{
			GameObject.Destroy(coll.gameObject);
			GameObject.Destroy(this.gameObject);
		}

		if (coll.gameObject.layer.Equals(12) || coll.gameObject.layer.Equals(10))
		{
			GameObject.Destroy(this.gameObject);
		}

		if (coll.gameObject.name.Equals("Ground"))
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnDestroy()
	{
		Instantiate(Resources.Load("MissileExplosion"), transform.position, transform.rotation);
	}
}
