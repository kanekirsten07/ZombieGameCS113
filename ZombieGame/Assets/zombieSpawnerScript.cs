using UnityEngine;
using System.Collections;

public class zombieSpawnerScript : MonoBehaviour {


	private bool canSpawnZombie;
	private double secstoWait = 5.0;
	GameObject spawner;
	public bool isTiming = false;
	private double timer;
	public GameObject Walker;
	public GameObject Nurse;
	public GameObject Cop;
	public GameObject Flood;
	public GameObject Minion;
	Transform spawnPosition;
	// Use this for initialization
	void Start () {


		spawnZombies();
	}

	void beginTimer()
	{
		timer = 0;
		isTiming = true;
	}

	// Update is called once per frame
	void Update () {
		if(isTiming)
		{
			timer += Time.deltaTime;
		}
		if(timer > secstoWait)
		{
			
			canSpawnZombie = true;
		}
	}

	void evaluateCondishuns()
	{
		// Evaluate mah condishuns to determine which zombies spawn and how many
		if(canSpawnZombie)
		{
			spawnZombies();
		}
	}

	void spawnZombies()
	{

		 // don't want the bullet spawn in centre
		float spawnDistance = 2.0f;
		 
		Debug.Log("Spawner position:"+ transform.position);
		// ...   
		GameObject.Instantiate(Walker, transform.position + spawnDistance * transform.forward, transform.rotation);
	}
}
	