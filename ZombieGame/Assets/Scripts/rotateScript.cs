using UnityEngine;
using System.Collections;

public class rotateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 chronoStopRotation = new Vector3(0,0,1);
		transform.Rotate(chronoStopRotation);
	}
}
