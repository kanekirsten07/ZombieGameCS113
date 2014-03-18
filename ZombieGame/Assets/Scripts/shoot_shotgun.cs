using UnityEngine;
using System.Collections;

public class shoot_shotgun : MonoBehaviour {

	public GameObject bullet;
	public int limit_shots_per_sec;
	private int framePassed = 0;
	public int angle; // in degrees
	public int number_of_bullets;
	public AudioClip sound;
	public int currentAmmo;
	public int startingAmmo;

	public GameInfoScript gis;
	
	// Use this for initialization
	void Start () {
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
		currentAmmo = startingAmmo = 10;

		angle = 5;
		number_of_bullets = 7;
		limit_shots_per_sec = 1;
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
		
		// only fire a bullet every 12 frames. AKA, 5 bullets per frame
		if (framePassed == (60/limit_shots_per_sec) && Input.GetMouseButtonDown (0)) 
		{

			fire ();
			framePassed = 0;
			
		} 	
		

	}
	
	void fire()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		currentAmmo--;

		AudioSource.PlayClipAtPoint (sound, transform.position);
		
		GameObject zombini = GameObject.Find ("Zombini");
		
		bool  temp = zombini.GetComponent<ZombiniController>().facingRight;
		
		Vector3 spawnLocation = GameObject.Find("sphere_shotgun").transform.position;

		// instantiate 7 bullets
		// Vector3 init_vector = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z)).normalized;
		Vector3 init_vector;
		Vector3 temp2 = new Vector3(1,0,2);
		Vector3 temp3 = new Vector3(-1,0,2);
		
		if(mousePos.x > transform.position.x)
		{
			init_vector = temp2;
		}
		else
			init_vector = temp3;

		// create first vector, call this depending on how many bullets there are
		init_vector = Quaternion.AngleAxis (angle, Vector3.back) * init_vector;
		init_vector = Quaternion.AngleAxis (angle, Vector3.back) * init_vector;
		init_vector = Quaternion.AngleAxis (angle, Vector3.back) * init_vector;

		for (int i = 0; i < number_of_bullets; i++) 
		{
			// init bullet
			bullet = (GameObject)Instantiate(Resources.Load("prefab_bullet"), spawnLocation, transform.rotation);
			// set velocity from top bullet to bottom
			bullet.GetComponent<Bullet>().velocity = init_vector;
			// get next velocity
			init_vector = Quaternion.AngleAxis(angle, Vector3.forward) * init_vector;
			// set bullet speed
			bullet.GetComponent<Bullet> ().velocity /= 2;
			// set Z axis of each bullet
			bullet.GetComponent<Bullet> ().velocity.z = 1;
		}

	}
	
}
