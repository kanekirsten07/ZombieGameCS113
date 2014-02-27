using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour {

	public bool isTiming = false;
	private double timer;
	public int waveNum = 1;
	private double secstoWait = 31;
	// Use this for initialization
	void Start () {
		beginTimer();
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
			
			waveNum++;
			
		}
	
	}
}
