using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfoScript : MonoBehaviour {


	public List<GameObject> powerUpList;
	// Use this for initialization

	//boolean powerups
	public bool powerUpActive;
	public bool chronoStopActive;
	public bool repulsorActive;
	public bool jetPackActive;
	public bool overPowerActive;

	public float powerUpDuration;
	public float powerUpTimer;

	public GameObject playerInventoryItem;

	public GameObject player;

	public Vector3 inventorySlotLocation;

	void Start () {
		powerUpList = new List<GameObject>();
		//powerUpList.Add((GameObject)(Resources.Load("MedKit")));
		//powerUpList.Add((GameObject)(Resources.Load("Repulsor")));
		powerUpList.Add((GameObject)(Resources.Load("JetPack")));
	///	powerUpList.Add((GameObject)(Resources.Load("Overpower")));
	//	powerUpList.Add((GameObject)(Resources.Load("ChronoStop")));
		powerUpList.Add((GameObject)(Resources.Load("Pistol")));

		player = GameObject.FindGameObjectWithTag("Player");

		//set all to false to begin with
		powerUpActive = chronoStopActive = repulsorActive = jetPackActive =	overPowerActive = false;

		//set timers
		powerUpDuration = powerUpTimer = 10;

		inventorySlotLocation = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

	}
	
	// Update is called once per frame
	void Update () {
		if (powerUpTimer > 0 && powerUpActive)
		{
			applyPowerUpEffect();
			powerUpTimer -= Time.deltaTime;
		}

		if (powerUpTimer <= 0)
		{
			powerUpTimer = powerUpDuration;
			switchPowerUpOff();
			powerUpActive = false;
		}

		player = GameObject.FindGameObjectWithTag("Player");

		if (playerInventoryItem != null)
			playerInventoryItem.transform.position = player.transform.position;
	}

	void applyPowerUpEffect()
	{
		if (chronoStopActive)
		{

		}

		else if (repulsorActive)
		{

		}

		else if (jetPackActive)
		{
			player.rigidbody2D.gravityScale = 0.5f;
		}

		else if (overPowerActive)
		{

		}
	}

	public void switchPowerUpOff()
	{
		if(chronoStopActive)
		{
			chronoStopActive = false;
		}
		
		else if (repulsorActive)
		{
			repulsorActive = false;
		}
		
		else if (jetPackActive)
		{
			jetPackActive = false;
			player.rigidbody2D.gravityScale = 1f;
		}
		
		else if (overPowerActive)
		{
			overPowerActive = false;
		}
	}

	public List<GameObject> getPowerUpList()
	{
		return powerUpList;
	}
}
