using UnityEngine;
using System.Collections;


public class PowerUpTimerScript : MonoBehaviour {


	float timer;
	public GameInfoScript gis;

	// Use this for initialization
	void Start () {
		gis = (GameInfoScript)GameObject.Find("GameWorld").GetComponent<GameInfoScript>() as GameInfoScript;
		timer = gis.powerUpTimer;
	}
	
	// Update is called once per frame
	void Update () {
		if(gis.playerInventoryItem != null)
			this.guiText.text = "";

		else if (gis.powerUpActive)
		{
			timer = gis.powerUpTimer;
			string precision = timer.ToString();
		
			if (gis.powerUpTimer >= 10.0f)
			{
				Debug.Log (precision.Length);
				precision = precision.Substring(0,4);
			}

			else
				precision = precision.Substring(0,3);				
			

			precision = precision + " sec";
			this.guiText.text = precision;
		}

		else
			this.guiText.text = "";
	}
}
