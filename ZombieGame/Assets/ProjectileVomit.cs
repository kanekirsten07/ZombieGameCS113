using UnityEngine;
using System.Collections;

public class ProjectileVomit : MonoBehaviour {

	private CopScript cop;

	
	float timer;

	// Use this for initialization
	void Start () {
		timer = 2.0f;
		cop = (CopScript) FindObjectOfType(typeof(CopScript));
		if(cop.facingRight)
		{
			Debug.Log("Blech right");
		rigidbody2D.AddForce(new Vector3(500,400,0)); 
	}else{
			Debug.Log("Blech left");
		rigidbody2D.AddForce(new Vector3(-500,400,0)); 
	}
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		
		if (timer <= 0.0f)
			GameObject.Destroy (this.gameObject);
		

		
		// Debug.Log ("velocity= "+velocity);
		


	}
}
