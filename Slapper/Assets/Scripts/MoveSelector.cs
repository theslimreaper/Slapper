using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveSelector : MonoBehaviour {
	public int currentLightMove;//current move that will be used
	public int currentHeavyMove;
	int previousLightMove;//current move is put here during transition for comparison purposes
	int previousHeavyMove;
	int nextLightMove;//new move is put here to compare to current move before being shifted in
	int nextHeavyMove;
	public bool[] lightMovesAvailable;//list of all move sets availability
	public bool[] heavyMovesAvailable;//list of all heavy moves availability
	int availableLights=0;
	int availableHeavies=0;
	public Text currentDisplay;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeLightAttack()//call at the end of the third animation in the chain to switch 
	{
		availableLights = 0;
		for(int i=0;i<lightMovesAvailable.Length;i++)
		{
			if(lightMovesAvailable[i]==true)
				availableLights++;
		}
		if(Random.Range(1,3)==1&&availableLights>1)//select a new attack 50% of the time
		{
			previousLightMove=currentLightMove;//make it so the previous wont be selected 
			while(currentLightMove==previousLightMove)//while you still have the old move
			{
				nextLightMove=Random.Range(0,lightMovesAvailable.Length);//check to see if the move is unlocked
				if(lightMovesAvailable[nextLightMove]==true)
						currentLightMove=nextLightMove;//sets new move 
			}
			//set animation parameter to current light move here
			print("new light attack: "+currentLightMove);
			currentDisplay.text = "Current Light Attack:" + currentLightMove + "\nCurrent Heavy Attack: " + currentHeavyMove;
		}
	}
	public void changeHeavyAttack()//call after the end of the heavy attack animation
	{
		availableHeavies = 0;
		for(int i=0;i<heavyMovesAvailable.Length;i++)
		{
			if(heavyMovesAvailable[i]==true)
				availableHeavies++;
		}
		if(Random.Range(1,3)==1&&availableHeavies>1)//select a new attack 50% of the time
		{
			previousHeavyMove=currentHeavyMove;//make it so the previous wont be selected 
			while(currentHeavyMove==previousHeavyMove)//while you still have the old move
			{
				nextHeavyMove=Random.Range(0,heavyMovesAvailable.Length);//checks to see if moves unlocked
					if(heavyMovesAvailable[nextHeavyMove]==true)
						currentHeavyMove=nextHeavyMove;//sets new move
			}
		}
		//set animation parameter to currentheavymove here
		print ("new heavy attack: " + currentHeavyMove);
		currentDisplay.text = "Current Light Attack:" + currentLightMove + "\nCurrent Heavy Attack: " + currentHeavyMove;
	}



	public void AvailableMoves(int amount)//call to set up available moves either 1, 3, or 5 of both 
	{
		for(int i=0;i<amount;i++)
		{
			lightMovesAvailable[i]=true;
			heavyMovesAvailable[i]=true;
		}
		if(amount<lightMovesAvailable.Length)//if the current amount is shorter than the current amount  set extras to false
		{
			for(int i=amount;i<lightMovesAvailable.Length;i++)
			{
				lightMovesAvailable[i]=false;
			}
		}
		if(amount<heavyMovesAvailable.Length)//if the current amount is shorter than the current amount  set extras to false
		{
			for(int i=amount;i<heavyMovesAvailable.Length;i++)
			{
				heavyMovesAvailable[i]=false;
			}
		}

		for(int i=0;i<heavyMovesAvailable.Length;i++)
			print ("move set "+i+" is set to "+heavyMovesAvailable[i]);

		currentDisplay.text = "Current Light Attack:" + currentLightMove + "\nCurrent Heavy Attack: " + currentHeavyMove;
	}
}
