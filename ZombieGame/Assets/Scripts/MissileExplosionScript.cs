using UnityEngine;
using System.Collections;

public class MissileExplosionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.layer == 10) //layer 10 == "Enemies"
		{
			GameObject.Destroy(coll.gameObject);
		}

	}
}
