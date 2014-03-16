using UnityEngine;
using System.Collections;

public class InventoryRepulsorScript : MonoBehaviour {
	
	
	public GameInfoScript gis;
	// Use this for initialization
	void Start () {
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
		{
			GetComponent<AudioSource>().Play();
			if (gis.powerUpActive)
				gis.switchPowerUpOff();
			
			GameObject.Destroy(this.gameObject);
			gis.repulsorActive = true;
			gis.powerUpActive = true;
			
		}
	}
}
