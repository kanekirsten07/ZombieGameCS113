using UnityEngine;
using System.Collections;

public class shoot_missileLauncher : MonoBehaviour {

	public GameObject missile;
	public int limit_shots_per_sec;
	private float timePassed = 0.0f;

	public int currentAmmo;
	public int startingAmmo;

	public GameInfoScript gis;

	// Use this for initialization
	void Start () {
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
		startingAmmo = currentAmmo = 10;
			
		limit_shots_per_sec = 2;
	}
	
	// Update is called once per frame
	void Update () {

		if (currentAmmo == 0)
		{
			GameObject.Destroy(this.gameObject);
			gis.playerWeapon = 
				(GameObject)Instantiate(Resources.Load("prefab_pistol"), gis.inventorySlotLocation, transform.rotation);
		}

		if (Input.GetMouseButtonDown (0)) 
		{
			fire ();
		}
	
	}

	void fire()
	{
		Vector3 spawnLocation = transform.position; 
		
		// if(facingRight)	spawnLocation += (2 * Vector3.right);
		// else 			spawnLocation += (2 * Vector3.left);
		
		missile = ((GameObject)Instantiate (Resources.Load ("prefab_missile"), spawnLocation, transform.rotation));

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 1;
		missile.GetComponent<Missile> ().velocity = (mousePos - transform.position).normalized;
		
	}
}
