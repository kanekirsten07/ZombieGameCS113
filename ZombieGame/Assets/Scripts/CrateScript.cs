using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrateScript : MonoBehaviour {

	private bool isQuitting;
	// Use this for initialization
	void Start () {
		isQuitting = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnDestroy()
	{
		if(!isQuitting)
			spawnRandomItem();
	}

	void spawnRandomItem()
	{
		//if(Random.Range(0, 1) < 0.5f)
			spawnRandomPowerUp();
		//else
			//spawnRandomWeapon();
	}

	void spawnRandomPowerUp()
	{
		GameInfoScript gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
		List<GameObject> powerUpList = gis.getPowerUpList();
		int randIndex = Random.Range(0, powerUpList.Count -  1);
		Quaternion facingUp = new Quaternion(0,0,0,1);
		GameObject.Instantiate(powerUpList[randIndex], transform.position, facingUp);
	}

	void spawnRandomWeapon()
	{

	}

	void OnApplicationQuit()
	{
		isQuitting = true;
	}



}
