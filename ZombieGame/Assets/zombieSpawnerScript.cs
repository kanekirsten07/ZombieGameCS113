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
	private double zombietimerdivision = 4;
	Transform spawnPosition;
	// Use this for initialization


	private int walkerBase = 0;
	private int walkerOffset = 100;
	private int copBase = 0;
	private int copOffsetNeg = 0;
	private int copOffsetPos = 0;
	private int nurseBase = 0;
	private int nurseOffsetNeg = 0;
	private int nurseOffsetPos = 0;
	private int FloodBase = 0;
	private int floodOffsetNeg = 0;
	private int floodOffsetPos = 0;

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
		int spawnSeed = Random.Range(0, 100);
		/*
		Debug.Log("Spawner position:"+ transform.position);
			*/
		if(spawnSeed> 0 && spawnSeed< 25)
			{
		GameObject.Instantiate(Walker, transform.position , transform.rotation);
		}else if(spawnSeed > 25 && spawnSeed< 50)
		{
			GameObject.Instantiate(Nurse, transform.position , transform.rotation);
		}else if(spawnSeed > 50 && spawnSeed < 75)
		{
			GameObject.Instantiate(Flood, transform.position , transform.rotation);
		}else if(spawnSeed > 75 && spawnSeed < 100)
		{
			GameObject.Instantiate(Cop, transform.position , transform.rotation);
		}

	}
}
	