using UnityEngine;
using System.Collections;

public class ShotgunScript : MonoBehaviour {

	public GameInfoScript gis;

	// Use this for initialization
	void Start () {
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Zombini" && Input.GetKeyDown(KeyCode.Q))
		{
			if (gis.playerWeapon.tag.Equals("shotgun"))
			{
				gis.playerWeapon.GetComponent<shoot_shotgun>().currentAmmo += 
					gis.playerWeapon.GetComponent<shoot_shotgun>().startingAmmo;
			}

			else
			{
				GameObject.Destroy(gis.playerWeapon);
				gis.playerWeapon = (GameObject)Instantiate(Resources.Load("prefab_shotgun"), gis.inventorySlotLocation, transform.rotation);
			}

			GameObject.Destroy(this.gameObject);
		}
	}
}
