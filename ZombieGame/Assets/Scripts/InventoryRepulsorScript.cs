using UnityEngine;
using System.Collections;

public class InventoryRepulsorScript : MonoBehaviour {
	
	public AudioClip ac;
	public GameInfoScript gis;
	// Use this for initialization
	void Start () {
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
		{
			AudioSource.PlayClipAtPoint(ac, GameObject.Find ("Zombini").transform.position);
			if (gis.powerUpActive)
				gis.switchPowerUpOff();
			
			GameObject.Destroy(this.gameObject);
			gis.repulsorActive = true;
			gis.powerUpActive = true;
			
		}
	}
}
