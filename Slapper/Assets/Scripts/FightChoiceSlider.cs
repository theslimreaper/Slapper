using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightChoiceSlider : MonoBehaviour {
	public Camera mainCamera;
	public Image header;
	public Image quote;
	public Button fightButton;

	public GameObject firstCharacter;
	public Sprite firstHeader;
	public Sprite firstQuote;
	public static bool firstCompleted = true;

	public GameObject secondCharacter;
	public Sprite secondHeader;
	public Sprite secondQuote;
	public static bool secondCompleted=false;

	public GameObject thirdCharacter;
	public Sprite thirdHeader;
	public Sprite thirdQuote;
	public static bool thirdCompleted = false;

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
		if (currentFightNumber == 1) {
			mainCamera.transform.position=Vector3.Lerp(startPos, new Vector3 (1,1,-10),temp/timeToShift);//(startPos.x-transform.position.x)/startPos.x)
			temp+=Time.deltaTime;
			firstCharacter.renderer.material.SetColor ("_Color", Color.white);
			secondCharacter.renderer.material.SetColor ("_Color", Color.black);
			thirdCharacter.renderer.material.SetColor ("_Color", Color.black);

			header.sprite=firstHeader;
			quote.sprite=firstQuote;
			fightButton.interactable=true;
			}
		if (currentFightNumber == 2) {
			mainCamera.transform.position=Vector3.Lerp(startPos, new Vector3 (19,1,-10),temp/timeToShift);	
			temp+=Time.deltaTime;
			firstCharacter.renderer.material.SetColor ("_Color", Color.black);
			secondCharacter.renderer.material.SetColor ("_Color", Color.black);
			thirdCharacter.renderer.material.SetColor ("_Color", Color.black);

			header.sprite=secondHeader;
			quote.sprite=secondQuote;

			if(secondCompleted)
				fightButton.interactable=true;
			else
				fightButton.interactable=false;
			}
		if (currentFightNumber == 3) {
			mainCamera.transform.position=Vector3.Lerp(startPos, new Vector3 (34,1,-10),temp/timeToShift);
			temp+=Time.deltaTime;
			firstCharacter.renderer.material.SetColor ("_Color", Color.black);
			secondCharacter.renderer.material.SetColor ("_Color", Color.black);
			thirdCharacter.renderer.material.SetColor ("_Color", Color.black);

			header.sprite=thirdHeader;
			quote.sprite=thirdQuote;

			if(thirdCompleted)
				fightButton.interactable=true;
			else
				fightButton.interactable=false;
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

}
