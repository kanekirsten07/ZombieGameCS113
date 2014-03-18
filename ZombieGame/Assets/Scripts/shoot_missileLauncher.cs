using UnityEngine;
using System.Collections;

public class shoot_missileLauncher : MonoBehaviour {

	public GameObject missile;
	public int limit_shots_per_sec;
	private float timePassed = 0.0f;

	public int currentAmmo;
	public int startingAmmo;
	public int framePassed;

	public GameInfoScript gis;

	public AudioClip sound;

	// Use this for initialization
	void Start () {
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
		startingAmmo = currentAmmo = 10;
			
		limit_shots_per_sec = 2;
	}
	
	// Update is called once per frame
	void Update () {

		if (framePassed < (60/limit_shots_per_sec))
			framePassed++;

		if (currentAmmo == 0)
		{
			GameObject.Destroy(this.gameObject);
			gis.playerWeapon = 
				(GameObject)Instantiate(Resources.Load("prefab_pistol"), gis.inventorySlotLocation, transform.rotation);
		}

		if (framePassed == (60/limit_shots_per_sec) && Input.GetMouseButtonDown (0)) 
		{
			fire ();
			framePassed = 0;
		}
	
	}

	void fire()
	{
		currentAmmo--;

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		AudioSource.PlayClipAtPoint (sound, transform.position);
		
		GameObject zombini = GameObject.Find ("Zombini");
		
		bool  temp = zombini.GetComponent<ZombiniController>().facingRight;
		
		Vector3 spawnLocation = GameObject.Find("sphere_missile").transform.position;		

		missile = ((GameObject)Instantiate (Resources.Load ("prefab_missile"), spawnLocation, transform.rotation));

		Vector3 flip = new Vector3(-4, 4, 1);

		Vector3 temp2 = new Vector3(1,0,2);
		Vector3 temp3 = new Vector3(-1,0,2);

		if(mousePos.x > transform.position.x)
		{
			missile.transform.localScale = flip; 
			missile.GetComponent<Missile> ().velocity = temp2;
		}
		else
			missile.GetComponent<Missile> ().velocity = temp3;

		mousePos.z = 1;

		// missile.GetComponent<Missile> ().velocity = (mousePos - transform.position).normalized;

		// missile.GetComponent<Missile> ().velocity = temp2;
	}
}
