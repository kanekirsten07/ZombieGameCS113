using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrateScript : MonoBehaviour {

	public GameInfoScript gis;
	private bool isQuitting;

	// Use this for initialization
	void Start () {
		isQuitting = false;
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name.Equals("ChronoStop(Clone)") ||
		    coll.gameObject.name.Equals("MedKit(Clone)") ||
		    coll.gameObject.name.Equals("OverPower(Clone)") ||
		    coll.gameObject.name.Equals("Repulsor(Clone)") ||
		    coll.gameObject.name.Equals("JetPack(Clone)") ||

		    coll.gameObject.name.Equals("Spawn_machineGun(Clone)") ||
		    coll.gameObject.name.Equals("Spawn_shotgun(Clone)") ||
		    coll.gameObject.name.Equals("Spawn_missileLauncher(Clone)")

		    )
		{
			GameObject.Destroy(coll.gameObject);
		}

		if (coll.gameObject.name.Equals("prefab_bullet(Clone)") ||
		    coll.gameObject.name.Equals("prefab_missile(Clone)")
		    )
		{
			GameObject.Destroy(this.gameObject);
			GameObject.Destroy(coll.gameObject);
		}
	}

	void OnDestroy()
	{
		if(!isQuitting)
			spawnRandomItem();
	}

	void spawnRandomItem()
	{
		if(Random.Range(0, 2) == 1)
			spawnRandomPowerUp();
	  	else
			spawnRandomWeapon();
	}

	void spawnRandomPowerUp()
	{
		List<GameObject> powerUpList = gis.getPowerUpList();
		int randIndex = Random.Range(0, powerUpList.Count);
		Quaternion facingUp = new Quaternion(0,0,0,1);
		GameObject.Instantiate(powerUpList[randIndex], transform.position, facingUp);
	}

	void spawnRandomWeapon()
	{
		List<GameObject> weaponList = gis.getWeaponList();
		int randIndex = Random.Range(0, weaponList.Count);
		Quaternion facingUp = new Quaternion(0,0,0,1);
		GameObject.Instantiate(weaponList[randIndex], transform.position, facingUp);
	}

	void OnApplicationQuit()
	{
		isQuitting = true;
	}



}
