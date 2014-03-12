using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour {

	public bool isTiming = false;
	private double timer;
	public int waveNum ;
	private double secstoWait = 31;
	public bool is_carrying_missile_launcher = false;
	public bool is_carrying_laser_gun = false;
	public bool is_carrying_shotgun = false;
	public bool is_carrying_machine_gun = false;
	// Use this for initialization

	private GUIText Score;
	private GUIText wavenumGui;
	public int totalScore;
	void Start () {
		waveNum = 1;
		beginTimer();
		totalScore = 0;
		Score =  GameObject.Find("Score").guiText;
		wavenumGui =  GameObject.Find("waveNumber").guiText;
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
			timer = 0;
			
		}
		Score.text = "Score: " + totalScore.ToString();

		wavenumGui.text = "Wave Number: "+ waveNum.ToString();
		//Debug.Log(totalScore);
	
	}
}
