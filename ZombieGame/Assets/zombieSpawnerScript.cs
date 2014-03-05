using UnityEngine;
using System.Collections;

public class zombieSpawnerScript : MonoBehaviour {


	private bool canSpawnZombie = true;
	//31 because otherwise a conflict arises between
	// the smaller zombie spawns which ends up with no zombies spawning at all
	private double secstoWait = 31;
	GameObject spawner;
	public bool isTiming = false;
	public bool isTiming2 = false;
	private double timer;
	private double timer2;
	public GameObject Walker;
	public GameObject Nurse;
	public GameObject Cop;
	public GameObject Flood;
	public GameObject Minion;
	private double zombietimerdivision = 10;
	Transform spawnPosition;
	// Use this for initialization
	void Start () {


		beginTimer();
		beginTimer2();
		InvokeRepeating("evaluateCondishuns", 0, 1.0f);
	}

	void beginTimer()
	{
		timer = 0;
		isTiming = true;
	}
	void beginTimer2()
	{
		timer2 = 0;
		isTiming2 = true;
	}

	// Update is called once per frame
	void Update () {
		if(isTiming)
		{
			timer += Time.deltaTime;
			timer2 += Time.deltaTime;

		}
		if(timer > secstoWait)
		{
			
			canSpawnZombie = true;
			beginTimer();

		}else if(timer2 > zombietimerdivision)
		{
			canSpawnZombie = false;
			beginTimer2();
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
		/*
		GameObject go = GameObject.Find("Zombini");
		Transform target = go.transform;
		Vector2 player = target.position;
		//Debug.Log("Walker position:" +walker);
		Debug.Log("Player Position: " +player);
		 // don't want the bullet spawn in centre
		Debug.Log("Spawn Zombie");
		float spawnDistance = 2.0f;

		Debug.Log("Spawner position:"+ transform.position);
		// ...   
			*/
		GameObject.Instantiate(Walker, transform.position , transform.rotation);

	}
}
	