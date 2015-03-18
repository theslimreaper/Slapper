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
	public GameObject[] androidOnlyObjects;
	public static bool accelerometer=true;
	public Image accelerometerChecked;
	public GameObject dodgeLeftButton;
	public GameObject dodgeRightButton;
	public Sprite muted;
	public Sprite unmuted;
	public GameObject[] hideDuringOptions;
	public Image controlsMenu;
	// Use this for initialization
	void Start () {
	/*	if(GameObject.Find ("OptionsButton")!=null)
		{
			volumeBar.value = volumeLevel/100f;
			speedBar.value = gameSpeed;
		}*/
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		if (Application.platform != RuntimePlatform.Android)//if not building to android hide the buttons during the fight
		{
			for(int i=0;i<androidOnlyObjects.Length;i++)
			{
				androidOnlyObjects[i].SetActive(false);
			}
		}



		if(Application.loadedLevelName!="Main Menu" && Application.platform == RuntimePlatform.Android && accelerometer==false)//if on android and not in the main menu show the dodge buttons
		{
			dodgeRightButton.SetActive(true);
			dodgeLeftButton.SetActive(true);
		}
		if(Application.loadedLevelName!="Main Menu" && Application.platform == RuntimePlatform.Android && accelerometer==true)//if on android and not in the main menu show the dodge buttons
		{
			dodgeRightButton.SetActive(false);
			dodgeLeftButton.SetActive(false);
		}
		Time.timeScale = 1;
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
		{
			Options.gameObject.SetActive(false);
			Time.timeScale=gameSpeed;
			for(int i=0;i<hideDuringOptions.Length;i++)
			{
				hideDuringOptions[i].SetActive(true);
			}
		}
		else
		{
			Options.gameObject.SetActive(true);
			Time.timeScale=0;
			for(int i=0;i<hideDuringOptions.Length;i++)
			{
				hideDuringOptions[i].SetActive(false);
			}
		}
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
		//Time.timeScale = (value+.25f)/1.25f;
		gameSpeed = Time.timeScale=(value+.25f)/1.25f;
	//	print (Time.timeScale);
		speedIndicator.text = "Game Speed: " + (gameSpeed * 100.00f).ToString("f0")+"%";
	}

	public void toggleAccelerometer()
	{
		if (accelerometer == true)
		{
			accelerometerChecked.enabled=false;
			accelerometer = false;
			if(Application.loadedLevelName!="Main Menu" && Application.platform == RuntimePlatform.Android)//if on android and not in the main menu show the dodge buttons
			{
				dodgeRightButton.SetActive(true);
				dodgeLeftButton.SetActive(true);
			}
		}
		else
		{
			accelerometer=true;
			accelerometerChecked.enabled=true;
			if(Application.loadedLevelName!="Main Menu" && Application.platform == RuntimePlatform.Android)//if on android and not in the main menu show the dodge buttons
			{
				dodgeRightButton.SetActive(false);
				dodgeLeftButton.SetActive(false);
			}
		}

	}

	public void toggleMute()
	{
		if (AudioListener.volume==0)
		{
			AudioListener.volume=volumeLevel;
			Options.sprite=unmuted;
		}
		else
		{
			AudioListener.volume=0;
			Options.sprite=muted;
		}
	}

	public void toggleControls()
	{
		if (controlsMenu.IsActive())
		{
			controlsMenu.gameObject.SetActive(false);
			for(int i=0;i<hideDuringOptions.Length;i++)
			{
				hideDuringOptions[i].SetActive(true);
			}
		}
		else
		{
			controlsMenu.gameObject.SetActive(true);
			for(int i=0;i<hideDuringOptions.Length;i++)
			{
				hideDuringOptions[i].SetActive(false);
			}
		}
	}

}
