using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Vector3 velocity;

	float timer;

	// Use this for initialization
	void Start () {

		timer = 1000.0f;
	}
	
	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;

		if (timer <= 0.0f)
				GameObject.Destroy (this.gameObject);
		
		transform.Translate(velocity*5);

		// Debug.Log ("velocity= "+velocity);
	
		// float angle = ((Mathf.Atan2 (velocity.y, velocity.x)* Mathf.Rad2Deg));

		// transform.rotation = Quaternion.Euler (0, 0, angle); 

		// Debug.Log ("transform.rotation = "+transform.rotation);
		// Debug.Log ("degrees= " + Mathf.Atan2 (velocity.y, velocity.x) * Mathf.Rad2Deg);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name.Equals("Crate(Clone)"))
		{
			GameObject.Destroy(coll.gameObject);
			GameObject.Destroy(this.gameObject);
		}
	}
	
}
