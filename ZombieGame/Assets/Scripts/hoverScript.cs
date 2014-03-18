using UnityEngine;
using System.Collections;

public class hoverScript : MonoBehaviour {


	//private variables
	private float topLimit;
	private float bottomLimit;
	private float deltaY; 

	// Use this for initialization
	void Start () {
		topLimit = (float)(transform.position.y + .2);
		bottomLimit = (float)(transform.position.y - .2);
		deltaY = 0.005f;
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y >= topLimit)
			deltaY = -.005f;

		else if (transform.position.y <= bottomLimit)
			deltaY = .005f;

		transform.Translate(0, deltaY, 0);
	
	}
}
