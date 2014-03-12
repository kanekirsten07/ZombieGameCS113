using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public Vector3 velocity;
	
	public float timer;

	// Use this for initialization
	void Start () {
	
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
		transform.Translate(velocity);
	}
}
