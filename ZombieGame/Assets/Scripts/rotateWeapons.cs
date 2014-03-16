using UnityEngine;
using System.Collections;

public class rotateWeapons : MonoBehaviour {

	private Vector3 mousePos;
	public GameObject zombini;
	private bool flipped;

	private Vector3 flip;

	// Use this for initialization
	void Start () {
		zombini = GameObject.Find("Zombini");
		// flip = transform.localScale;
		// flip.x *= -1;

		flipped = false;

		// if (zombini.GetComponent <ZombiniController> ().facingRight)
		//	transform.localScale = flip;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = zombini.transform.position;

		if (transform.position.x < mousePos.x && !flipped)
		{
			Debug.Log("flip right");
			flip = transform.localScale;
			flip.x *= -1;
			transform.localScale = flip;
			flipped = true;
		}

		if (transform.position.x > mousePos.x && flipped)
		{
			Debug.Log("flip left");
			flip = transform.localScale;
			flip.x *= -1;
			transform.localScale = flip;
			flipped = false;
		}



		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if(flipped)
			transform.eulerAngles = 
			new Vector3(0,0,Mathf.Atan2((mousePos.y - transform.position.y),
			                            (mousePos.x - transform.position.x))*Mathf.Rad2Deg);
		else
			transform.eulerAngles = 
				new Vector3(0,0,Mathf.Atan2((mousePos.y - transform.position.y),
				                            (mousePos.x - transform.position.x))*Mathf.Rad2Deg + 180);

	
	}
}
