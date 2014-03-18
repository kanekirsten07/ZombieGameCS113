using UnityEngine;
using System.Collections;

public class MissileExplosionScript : MonoBehaviour {

	float lifetime;
	// Use this for initialization
	void Start () {
		lifetime = .1f;
	}
	
	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;
		if (lifetime <= 0f)
			GameObject.Destroy(this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.layer == 10) //layer 10 == "Enemies"
		{
			GameObject.Destroy(coll.gameObject);
		}

	}
}
