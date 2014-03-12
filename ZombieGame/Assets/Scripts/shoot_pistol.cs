using UnityEngine;
using System.Collections;

public class shoot_pistol : MonoBehaviour {

	public GameObject bullet;
	public int limit_shots_per_sec;
	private int framePassed = 0;

	public int ammo;
	public int magazine_size;
	private int current_ammo;

	// Use this for initialization
	void Start () {
	
		magazine_size = 15;
		ammo = 9999999;
		limit_shots_per_sec = 3;
	}
	
	// Update is called once per frame
	void Update () {

		if (framePassed < (60/limit_shots_per_sec))
			framePassed++;

		if (framePassed == (60/limit_shots_per_sec) && Input.GetMouseButtonDown (0)) 
		{
			if(current_ammo > 0)
				fire ();

			framePassed = 0;
		}

	}

	void reload()
	{
		if (ammo > magazine_size) 
		{
			current_ammo = magazine_size;
			ammo -= magazine_size;
		}
		else
		{
			current_ammo = ammo;
			ammo = 0;
		}


	}

	// shoot!!!!
	void fire()
	{
		current_ammo--;

		Vector3 spawnLocation = this.transform.position;

		bullet = (GameObject)Instantiate(Resources.Load("prefab_bullet"), spawnLocation, transform.rotation);

		bullet.GetComponent<Bullet>().velocity = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - new Vector3(this.transform.position.x, this.transform.position.y, 0)).normalized;
	
	}
}
