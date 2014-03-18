using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {
	
	public Vector3 mousePos;
	public Vector3 baseAngle = new Vector3(1, 0, 0);
	public BoxCollider2D collider2D;
	public float width;
	public float scale;

	public Vector3 gun_Pos;
	public GameObject closestGameObject;
	private float distance;

	void Start () {

		scale = 2000.0f;
		distance = Vector3.Distance (closestGameObject.transform.position, gun_Pos);

		collider2D = gameObject.GetComponent<BoxCollider2D>();
		width = collider2D.size.x;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		// initialize distance to a larger number
		distance = 10000.0f;

		// if a non enemy object was hit, do...
		if (!col.gameObject.tag.Equals ("Enemy"))
		{
			// if the object hit was closer than the previous one, then ...
			if(distance > Vector3.Distance (col.gameObject.transform.position, gun_Pos))
			{
				// update the new shortest distance
				distance = Vector3.Distance (col.gameObject.transform.position, gun_Pos);
				// update the closestGameObject
				closestGameObject = col.gameObject;
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		// get mouse position and set its Z axis to 1
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;
		mousePos.Normalize();
		mousePos.z = 1;

		// if the player is still shooting laser beam
		if (Input.GetMouseButtonDown (0))
		{
			// scale the energy beam
			Vector3 temp = new Vector3((int)(distance/2), transform.localScale.y, transform.localScale.z);
			transform.localScale = temp;
			// move the energy beam half way
			transform.Translate(mousePos * (transform.localScale.y / 2));
			// update the angle
			
		}

		// player stopped shooting laser beam
		if (Input.GetMouseButtonUp (0)) 
		{
			GameObject.Destroy(this.gameObject);
		}
	}
}
