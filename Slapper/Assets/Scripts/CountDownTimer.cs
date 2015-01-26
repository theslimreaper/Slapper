using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDownTimer : MonoBehaviour {
	public int maxTime=120;
	float currentTime;
	public Text timerText;
	public Image ResultsPage;
	// Use this for initialization
	void Start () {
		currentTime = maxTime;//start timer
		ResultsPage.gameObject.SetActive (false);//hide results page
	}
	
	// Update is called once per frame
	void Update () {
		timerText.text = "Time Remaining\n" + (int)currentTime;//display time left
		currentTime-= Time.deltaTime;//decrease timer by time since last call
		if(currentTime <= 0)//when timer runs out
		{
			Time.timeScale=0;//pause game
			timerEnd();//end of game function

		}
	}

	void timerEnd()
	{
		ResultsPage.gameObject.SetActive(true);//show results page
	}
}
