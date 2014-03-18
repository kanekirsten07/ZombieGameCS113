using UnityEngine;
using System.Collections;

public class SHOOT : MonoBehaviour {

	public GameObject bullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) 
			fire ();

	}

	// shoot!!!!
	void fire()
	{

		Vector3 spawnLocation = this.transform.position;

		bullet = (GameObject)Instantiate(Resources.Load("prefab_bullet"), spawnLocation, transform.rotation);

		bullet.GetComponent<Bullet>().velocity = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - new Vector3(this.transform.position.x, this.transform.position.y, 0)).normalized;
	
		Debug.Log ("Camera.main.ScreenToWorldPoint (Input.mousePosition)= "+Camera.main.ScreenToWorldPoint (Input.mousePosition));
	}
}
