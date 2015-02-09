using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuFunctions : MonoBehaviour {
	public Image Options;
	public Text volumeIndicator;
	public Text speedIndicator;
	public static float volumeLevel=100.0f;
	public Scrollbar volumeBar;
	public static float gameSpeed=1.0f;
	public Scrollbar speedBar;
	// Use this for initialization
	void Start () {
		if(GameObject.Find ("OptionsButton")!=null)
		{
			volumeBar.value = volumeLevel/100f;
			speedBar.value = gameSpeed;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LevelLoad (string levelName)
	{
		Application.LoadLevel(levelName);
	}

	public void OptionsToggle()
	{
		if (Options.IsActive())
			Options.gameObject.SetActive(false);
		else
			Options.gameObject.SetActive(true);
	}

	public void adjustVolume(float value)
	{
		AudioListener.volume = value*100f;
		volumeLevel = AudioListener.volume;
		volumeIndicator.text = "Volume: " + (value* 100f).ToString("f0") + "%";
		volumeIndicator.audio.Play ();
		print (value);
	}
	public void adjustGameSpeed(float value)
	{
		Time.timeScale = (value+.25f)/1.25f;
		gameSpeed = Time.timeScale;
		print (Time.timeScale);
		speedIndicator.text = "Game Speed: " + (gameSpeed * 100.00f).ToString("f0")+"%";
	}
}
