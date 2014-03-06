using UnityEngine;
using System.Collections;

public class PistolScript : MonoBehaviour {

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
		if (coll.gameObject.tag == "Player")
		{
			gis.playerInventoryItem = (GameObject)Instantiate(Resources.Load("Icons/IconPistol"), gis.inventorySlotLocation, transform.rotation);
			
			GameObject.Destroy(this.gameObject);
		}
	}
}
