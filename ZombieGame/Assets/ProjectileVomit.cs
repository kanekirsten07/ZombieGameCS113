using UnityEngine;
using System.Collections;

public class ProjectileVomit : MonoBehaviour {

	private CopScript cop;
	// Use this for initialization
	void Start () {
		cop = (CopScript) FindObjectOfType(typeof(CopScript));
		if(cop.facingRight)
		{
			Debug.Log("Blech right");
		rigidbody2D.AddForce(new Vector3(700,400,0)); 
	}else{
			Debug.Log("Blech left");
		rigidbody2D.AddForce(new Vector3(-700,400,0)); 
	}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
