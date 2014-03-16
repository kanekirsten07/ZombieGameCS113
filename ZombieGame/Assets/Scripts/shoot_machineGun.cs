using UnityEngine;
using System.Collections;

public class shoot_machineGun : MonoBehaviour {

	public GameObject bullet;
	private bool trigger = false;
	public int limit_shots_per_sec;
	private int framePassed = 0;

	public int ammo;
	public int magazine_size;
	public int currentAmmo;
	public int startingAmmo;

	public GameInfoScript gis;

	// Use this for initialization
	void Start () {
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
		startingAmmo = currentAmmo = 100;
		magazine_size = 100;
		ammo = 500;
		limit_shots_per_sec = 20;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentAmmo == 0)
		{
			GameObject.Destroy(this.gameObject);
			gis.playerWeapon = 
				(GameObject)Instantiate(Resources.Load("prefab_pistol"), gis.inventorySlotLocation, transform.rotation);
		}

		if(framePassed < (60/limit_shots_per_sec))
			framePassed++;

		// only fire a bullet every 6 frames. AKA, 10 bullets per frame
		if (framePassed == (60/limit_shots_per_sec) && (trigger || Input.GetMouseButtonDown (0))) 
		{
			if(currentAmmo > 0)
				fire ();

			trigger = true;
			framePassed = 0;
		} 	
	
		if (Input.GetMouseButtonUp (0)) 
		{
			trigger = false;
		}

	}

	void fire()
	{
		currentAmmo--; 

		Vector3 spawnLocation = this.transform.position;

		// if(facingRight)	spawnLocation += (2 * Vector3.right);
		// else 			spawnLocation += (2 * Vector3.left);

		spawnLocation.z = Camera.main.ScreenToWorldPoint (Input.mousePosition).z;
		
		bullet = (GameObject)Instantiate(Resources.Load("prefab_bullet"), spawnLocation, transform.rotation);
		
		bullet.GetComponent<Bullet>().velocity = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z)).normalized;

		bullet.GetComponent<Bullet> ().velocity *= 2;

		bullet.GetComponent<Bullet> ().velocity.z = 1;
	}

}
