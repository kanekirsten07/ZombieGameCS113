using UnityEngine;
using System.Collections;

public class CrateSpawnerScript : MonoBehaviour {

	bool timerCounting;
	bool alreadyTriggered;
	float spawnInterval;
	float timer;
	public GameObject crate;

	// Use this for initialization
	void Start () {
		alreadyTriggered = false;
		timerCounting = false;
		spawnInterval = 10;
		timer = spawnInterval;
		spawnCrate();
	}
	
	// Update is called once per frame
	void Update () {

		if (timerCounting)
			timer -= Time.deltaTime;

		if (crate == null)
		{
			timerCounting = true;
			if (timer <= 0.0f)
				spawnCrate();
		}

		if (timer <= 5 && !alreadyTriggered)
		{
			//create trigger
			GetComponent("ParticleSystem").particleSystem.Play();

			//switch trigger
			alreadyTriggered = true;
		}
	}

	void spawnCrate()
	{
		GetComponent("ParticleSystem").particleSystem.Stop();
		resetTimer();
		alreadyTriggered = false;
		crate = (GameObject)Instantiate(Resources.Load("Crate"), transform.position, transform.rotation);
	}

	void resetTimer()
	{
		timerCounting = false;
		timer = spawnInterval;
	}
}
