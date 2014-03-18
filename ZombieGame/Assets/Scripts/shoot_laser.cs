using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class shoot_laser : MonoBehaviour {

	public GameObject energy; 
	private bool trigger;

	public float time;

	// Use this for initialization
	void Start () {

		trigger = false;
		time = 30; // 30 seconds
	}


	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0))
		{
			fire ();
			time -= Time.deltaTime;
			trigger = true;
		}

		if(Input.GetMouseButtonUp(0))
			trigger = false;

		if(trigger)
			time -= Time.deltaTime;

		if (time <= 0)
		{
			GameObject.Destroy (this.gameObject);
		}
	}

	void fire()
	{
		Vector3 spawnLocation = transform.position; 
		
		// if(facingRight)	spawnLocation += (2 * Vector3.right);
		// else 			spawnLocation += (2 * Vector3.left);
			
		energy = ((GameObject)Instantiate (Resources.Load ("prefab_energy"), spawnLocation, transform.rotation));

		Vector3 temp = new Vector3 (4000, transform.localScale.y, transform.localScale.z);
		energy.transform.localScale = temp; 

		energy.GetComponent<Energy> ().gun_Pos = this.transform.position;
		energy.GetComponent<Energy> ().closestGameObject = this.gameObject;
	}
}
