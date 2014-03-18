using UnityEngine;
using System.Collections;

public class shoot_machineGun : MonoBehaviour {

	public GameObject bullet;
	private bool trigger = false;
	public int limit_shots_per_sec;
	private int framePassed = 0;

	public int ammo;
	public int currentAmmo;
	public int startingAmmo;

	public AudioClip sound;

	public GameInfoScript gis;

	// Use this for initialization
	void Start () {
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
		startingAmmo = currentAmmo = 100;
		ammo = 500;
		limit_shots_per_sec = 10;
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
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		AudioSource.PlayClipAtPoint (sound, transform.position);
		
		// currentAmmo--; 
		
		GameObject zombini = GameObject.Find ("Zombini");
		
		bool  temp = zombini.GetComponent<ZombiniController>().facingRight;
		
		Vector3 spawnLocation = GameObject.Find("sphere_machineGun").transform.position;	

		bullet = (GameObject)Instantiate(Resources.Load("prefab_bullet"), spawnLocation, transform.rotation);
		
		bullet.GetComponent<Bullet>().velocity = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - spawnLocation).normalized;

		Vector3 temp2 = new Vector3(1,0,2);
		Vector3 temp3 = new Vector3(-1,0,2);
		
		if(mousePos.x > transform.position.x)
		{
			bullet.GetComponent<Bullet> ().velocity = temp2;
		}
		else
			bullet.GetComponent<Bullet> ().velocity = temp3;

		bullet.GetComponent<Bullet> ().velocity.z = 1;
	}

}
