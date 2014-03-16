using UnityEngine;
using System.Collections;

public class AmmoTextScript : MonoBehaviour {


	public GameInfoScript gis;

	// Use this for initialization
	void Start () {
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (gis.playerWeapon.tag);
		if (gis.playerWeapon.tag.Equals("pistol"))
			this.guiText.text = "Ammo: Infinite";

		else if (gis.playerWeapon.tag.Equals("shotgun"))
			this.guiText.text = "Ammo: " + gis.playerWeapon.GetComponent<shoot_shotgun>().currentAmmo;
	
		else if (gis.playerWeapon.tag.Equals("machineGun"))
			this.guiText.text = "Ammo: " + gis.playerWeapon.GetComponent<shoot_machineGun>().currentAmmo;
	
		else if (gis.playerWeapon.tag.Equals("missileLauncher"))
			this.guiText.text = "Ammo: " + gis.playerWeapon.GetComponent<shoot_missileLauncher>().currentAmmo;
	
		else
			Debug.Log ("playerWeapon tag not recognized");
	}
}
