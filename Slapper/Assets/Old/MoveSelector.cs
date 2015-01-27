using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveSelector : MonoBehaviour {
	public int currentLightMove;//current move that will be used
	public int currentHeavyMove;
	public bool[] lightMovesAvailable;//list of all move sets availability
	public bool[] heavyMovesAvailable;//list of all heavy moves availability
	int newLightMove;
	int newHeavyMove;
	public Text currentDisplay;
	public GameObject Player;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = Player.GetComponent<Animator> ();//used to set parameters in animation tree
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeLightAttack()//call at the end of the third animation in the chain to switch 
	{

		do{
			newLightMove=Random.Range(0,lightMovesAvailable.Length);//check to see if the move is unlocked
			if(lightMovesAvailable[newLightMove]==true)
				currentLightMove=newLightMove;//sets new move 
		}while(lightMovesAvailable[currentLightMove]==false);//grab new moves until you grab one that is available
			//set animation parameter to current light move here
			print("new light attack: "+currentLightMove);

			currentDisplay.text = "Current Light Attack:" + currentLightMove + "\nCurrent Heavy Attack: " + currentHeavyMove;

	}
	public void changeHeavyAttack()//call after the end of the heavy attack animation
	{
			do{
				newHeavyMove=Random.Range(0,lightMovesAvailable.Length);
			if(heavyMovesAvailable[newHeavyMove]==true)
				currentHeavyMove=newHeavyMove;
		}while(heavyMovesAvailable[currentHeavyMove]==false);

	
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
