using UnityEngine;
using System.Collections;

public class ChronoStopScript : MonoBehaviour {
	
	
	//GameObject gameWorld;
	public GameInfoScript gis;
	public PowerUpGUIScript pug;
	
	// Use this for initialization
	void Start () {
		//gameWorld = GameObject.Find("GameWorld");
		//pug = (PowerUpGUIScript)GameObject.Find("PowerUpGUI").GetComponent<PowerUpGUIScript>() as PowerUpGUIScript;
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Zombini" && Input.GetKeyDown(KeyCode.Q))
		{
			if (gis.chronoStopActive)
			{
				gis.powerUpTimer += gis.powerUpDuration;
			}
			
			else
			{
				Quaternion facingUp = new Quaternion(0,0,0,1);
				gis.playerInventoryItem = (GameObject)Instantiate(Resources.Load("Icons/IconChronoStop"), gis.inventorySlotLocation, facingUp);
			}
			
			GameObject.Destroy(this.gameObject);
		}
	}
}
