using UnityEngine;
using System.Collections;

public class pulsateScript : MonoBehaviour {

	//private variables
	private float outerLimit;
	private float innerLimit;
	private float deltaScale; 

	// Use this for initialization
	void Start () {
		outerLimit = (float)(transform.localScale.x + 1);
		innerLimit = (float)(transform.localScale.x);
		deltaScale = 0.03f;
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.localScale.x >= outerLimit)
			deltaScale = -0.03f;
		
		else if (transform.localScale.x <= innerLimit)
			deltaScale = 0.03f;

		Vector3 translateScale = new Vector3(deltaScale, deltaScale, 0);
		transform.localScale += translateScale;
		
	}
}
