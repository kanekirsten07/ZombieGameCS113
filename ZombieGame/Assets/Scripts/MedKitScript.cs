using UnityEngine;
using System.Collections;

public class MedKitScript : MonoBehaviour {

	public PlayerHealth ph;
	// Use this for initialization
	void Start () {
		ph = (PlayerHealth)GameObject.Find("Zombini").GetComponent<PlayerHealth>() as PlayerHealth;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			if (ph.health >= 85f)
				ph.health = 100f;
			
			else
				ph.health += 15f;
			
			GameObject.Destroy(this.gameObject);
		}
	}
}
