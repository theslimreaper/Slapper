using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightChoiceSlider : MonoBehaviour {
	public Camera mainCamera;
	public Image header;
	public Image quote;
	public Button fightButton;
	public string[] fightScenes;

	public GameObject firstCharacter;
	public Sprite firstHeader;
	public Sprite firstQuote;
	public static bool firstCompleted = false;

	public GameObject secondCharacter;
	public Sprite secondHeader;
	public Sprite secondQuote;
	public static bool secondCompleted=false;

	public GameObject thirdCharacter;
	public Sprite thirdHeader;
	public Sprite thirdQuote;
	public static bool thirdCompleted = false;


	public Sprite noHeader;
	public Sprite noQuote;
	Vector3 startPos= new Vector3 (1,1,-10);
	int currentFightNumber=1;
	float temp=0.0f;
	public float timeToShift=.5f;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Z))
			previousFightButton ();
		if(Input.GetKeyDown(KeyCode.M))
			nextFightButton();
		if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.N)||Input.GetKeyDown(KeyCode.X))
			if(currentFightNumber==1||(currentFightNumber==2&&firstCompleted)||(currentFightNumber==3&&secondCompleted))
				Application.LoadLevel(fightScenes[currentFightNumber-1]);
			




		if (currentFightNumber == 1) {
			mainCamera.transform.position=Vector3.Lerp(startPos, new Vector3 (1,1,-10),temp/timeToShift);
			temp+=Time.deltaTime;

			header.sprite=firstHeader;
			quote.sprite=firstQuote;
			fightButton.interactable=true;
			}
		if (currentFightNumber == 2) {
			mainCamera.transform.position=Vector3.Lerp(startPos, new Vector3 (19,1,-10),temp/timeToShift);	
			temp+=Time.deltaTime;
			if(firstCompleted)
			{
			secondCharacter.renderer.material.SetColor ("_Color", Color.white);
				header.sprite=secondHeader;
				quote.sprite=secondQuote;
				fightButton.interactable=true;
			}
			else
			{
				secondCharacter.renderer.material.SetColor ("_Color", Color.black);
				header.sprite=noHeader;
				quote.sprite=noQuote;
				fightButton.interactable=false;
			}
		}


		if (currentFightNumber == 3) {
			mainCamera.transform.position=Vector3.Lerp(startPos, new Vector3 (34,1,-10),temp/timeToShift);
			temp+=Time.deltaTime;
			if(secondCompleted)
			{
				thirdCharacter.renderer.material.SetColor ("_Color", Color.white);
				header.sprite=thirdHeader;
				quote.sprite=thirdQuote;
				fightButton.interactable=true;
			}
			else
			{
				thirdCharacter.renderer.material.SetColor ("_Color", Color.black);
				header.sprite=noHeader;
				quote.sprite=noQuote;
				fightButton.interactable=false;
			}
		}
	}

	public void nextFightButton()
	{
	if (currentFightNumber < 3)
			currentFightNumber++;
		startPos = mainCamera.transform.position;
		temp = 0;
	}
	public void previousFightButton()
	{
		if(currentFightNumber>1)
			currentFightNumber--;
		startPos = mainCamera.transform.position;
		temp = 0;
	}
	public void loadFight()
		{
			Application.LoadLevel (fightScenes [currentFightNumber - 1]);
		}
}
