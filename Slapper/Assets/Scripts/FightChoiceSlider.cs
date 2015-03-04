using UnityEngine;
using System.Collections;

public class FightChoiceSlider : MonoBehaviour {
	Vector3 startingLocation;
	Vector3 endLocation;
	bool needsMovement;
	bool hasMoved=false;
	int  totalFights=3;
	int currentFight = 1;
	public int speed=5;
	bool direction;//positive for next negative for previous
	// Use this for initialization
	void Start () {
		startingLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if(needsMovement)
		{
			hasMoved=true;
			transform.position=new Vector3 (transform.position.x+speed,transform.position.y,transform.position.z);
			if (this.transform.position==endLocation||(transform.position.x>endLocation.x&&direction)||transform.position.x<endLocation.x && !direction)
				needsMovement=false;
		}
	}

	public void nextFightButton(){
		if(currentFight<totalFights&&needsMovement==false)
		{
			needsMovement=true;
			endLocation=new Vector2(transform.position.x-1500,transform.position.y);
			print (endLocation.x+" "+endLocation.y);
			startingLocation=transform.position;
			if(speed>0)
				speed*=-1;
			currentFight++;
			direction=true;
		}
	}
	public void previousFightButton(){
		if (currentFight > 1&&needsMovement==false) 
		{
			needsMovement=true;
			endLocation=new Vector3(transform.position.x+1500,transform.position.y,transform.position.z);
			startingLocation=transform.position;
			if(speed<0)
				speed*=-1;
			currentFight--;
		}
	}
	public void incrementCurrent(){
		if(currentFight<totalFights)
			currentFight++;
	}
}
