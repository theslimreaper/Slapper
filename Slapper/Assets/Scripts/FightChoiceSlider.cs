using UnityEngine;
using System.Collections;

public class FightChoiceSlider : MonoBehaviour {
	Vector3 startingLocation;
	Vector3 endLocation;
	static bool needsMovement;
	public int fightNumber=1;
	bool hasMoved=false;
	public static int totalFights=3;
	public static int currentFight = 1;
	public int speed=5;
	// Use this for initialization
	void Start () {
		startingLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x<=0&&hasMoved==true)
		{
			needsMovement=false;
		}
		if(needsMovement)
		{
			hasMoved=true;
			transform.position=new Vector3 (transform.position.x+speed,transform.position.y,transform.position.z);

		}
	}

	public void nextFightButton(){
		if(fightNumber<totalFights)
		{
			needsMovement=true;
			endLocation=new Vector2(0,transform.position.y);
			print (endLocation.x+" "+endLocation.y);
			startingLocation=transform.position;
			if(speed>0)
				speed*=-1;
		}
	}
	public void previousFightButton(){
		if (currentFight > 1) 
		{
			needsMovement=true;
			endLocation=new Vector3(transform.position.x+1500,transform.position.y,transform.position.z);
			startingLocation=transform.position;
			if(speed<0)
				speed*=-1;
		}
	}
	public void incrementCurrent(){
		if(currentFight<totalFights)
			currentFight++;
	}
}
