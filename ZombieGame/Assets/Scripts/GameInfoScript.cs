using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfoScript : MonoBehaviour {


	public List<GameObject> powerUpList;
	public List<GameObject> weaponList;
	// Use this for initialization

	//boolean powerups
	public bool powerUpActive;
	public bool chronoStopActive;
	public bool repulsorActive;
	public bool jetPackActive;
	public bool overpowerActive;

	public bool applied;

	public float powerUpDuration;
	public float powerUpTimer;

	public GameObject playerInventoryItem;

	public GameObject player;

	public GameObject playerWeapon;
	public Vector3 inventorySlotLocation;

	public BoxCollider2D repulsorBox;

	void Start () {
		inventorySlotLocation = Camera.main.ScreenToWorldPoint
			(new Vector3(400,130, Camera.main.nearClipPlane));
		playerWeapon = (GameObject)Instantiate(Resources.Load("prefab_pistol"), inventorySlotLocation, transform.rotation);

		applied = false;
		powerUpList = new List<GameObject>();
		//powerUpList.Add((GameObject)(Resources.Load("MedKit")));
		powerUpList.Add((GameObject)(Resources.Load("Repulsor")));
		powerUpList.Add((GameObject)(Resources.Load("JetPack")));
		powerUpList.Add((GameObject)(Resources.Load("Overpower")));
		powerUpList.Add((GameObject)(Resources.Load("ChronoStop")));

		//weaponList.Add((GameObject)(Resources.Load("Icons/Spawn_machineGun")));
		//weaponList.Add((GameObject)(Resources.Load("Icons/Spawn_missileLauncher")));
		//weaponList.Add((GameObject)(Resources.Load("Icons/Spawn_shotgun")));


		player = GameObject.FindGameObjectWithTag("Zombini");

		//set all to false to begin with
		powerUpActive = chronoStopActive = repulsorActive = jetPackActive =	overpowerActive = false;

		//set timers
		powerUpDuration = powerUpTimer = 10;

		//inventorySlotLocation = new Vector3(player.transform.position.x, -20f, 0);

	}
	
	// Update is called once per frame
	void Update () {
		if (powerUpTimer > 0 && powerUpActive)
		{
			if(!applied)
			{
				applied = true;
				applyPowerUpEffect();
			}
			
			powerUpTimer -= Time.deltaTime;
		}
		
		if (powerUpTimer <= 0)
		{
			powerUpTimer = powerUpDuration;
			switchPowerUpOff();
			powerUpActive = false;
		}

		if (playerWeapon != null)
			playerWeapon.transform.position = player.transform.position;

		if (playerInventoryItem != null)
		{
			Vector3 wv = Camera.main.ScreenToWorldPoint(new Vector3(400,150,0));
			Debug.Log (wv);
			playerInventoryItem.transform.position = wv;
		}
		
		if (repulsorActive)
			growRepulsorBox();
	}

	void applyPowerUpEffect()
	{
		if (chronoStopActive)
		{
			//do nothing
		}

		else if (repulsorActive)
		{
			repulsorBox = (BoxCollider2D)GameObject.Find("Zombini").AddComponent("BoxCollider2D");
			repulsorBox.size = new Vector2 (0.8f, 0.8f);
			repulsorBox.center = new Vector2 (0.1f, .5f);
		}

		else if (jetPackActive)
		{
			player.rigidbody2D.gravityScale = 0.5f;
		}

		else if (overpowerActive)
		{
			//do nothing
		}
	}

	public void switchPowerUpOff()
	{
		applied = false;
		if(chronoStopActive)
		{
			chronoStopActive = false;
		}
		
		else if (repulsorActive)
		{
			repulsorActive = false;
			GameObject.Destroy(repulsorBox);
		}
		
		else if (jetPackActive)
		{
			jetPackActive = false;
			player.rigidbody2D.gravityScale = 1f;
		}
		
		else if (overpowerActive)
		{
			overpowerActive = false;
		}
	}

	public List<GameObject> getPowerUpList()
	{
		return powerUpList;
	}

	public List<GameObject> getWeaponList()
	{
		return weaponList;
	}

	void growRepulsorBox()
	{
		//if (repulsorBox.size.y < 2.5f)
			//repulsorBox.size = new Vector3(repulsorBox.size.x, repulsorBox.size.y +.05f, 0);
		if (repulsorBox.size.x < 2.5f)
			repulsorBox.size = new Vector3(repulsorBox.size.x + .05f, repulsorBox.size.y, 0);
	}
}
