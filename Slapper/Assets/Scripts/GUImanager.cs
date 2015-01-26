using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUImanager : MonoBehaviour {
	int score;
	int comboMultiplier=1;
	public int lightPower;
	public int heavyPower;
	public int superPower;
	bool blocking=false;
	public int totalBlockFrames;
	int remainingBlockFrames=0;
	public Text scoreboard;
	public Text multiplierDisplay;
	public Scrollbar comboMeter;
	public Scrollbar SpecialMeter;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (remainingBlockFrames <= 0&&blocking==true)//if the character is blocking and the block has expired set blocing status to false
			blocking = false;
		else if(remainingBlockFrames >0)//if there are more blocking frames available keep counting down
			remainingBlockFrames--;

		comboMeter.size = Fader.canvas.alpha;

	}

	public void lightAttack()//when the light attack button is pressed
	{
		score += lightPower*comboMultiplier;//add to the players score
		if(SpecialMeter.size<1)//increase the special meter bar if it isnt full
			SpecialMeter.size+=.03f;
		onHit ();//call the onHit functon
	}


	public void heavyAttack()//when the heavy attack button is pressed
	{
		score += heavyPower*comboMultiplier;//add to the players score
		if(SpecialMeter.size<1)//increase the specal meter bar if it isnt full
			SpecialMeter.size+=.06f;
		onHit ();//call on hit

	}


	public void superAttack()//if the super attack button is pressed
	{
		if(SpecialMeter.size>=1)//if the bar is full perform the super attack
		{
			score += superPower*comboMultiplier;//add a large amount to the score
			onHit ();//call on hit
			SpecialMeter.size=0;//reset the bar

		}


	}
	public void block()
	{
		blocking = true;//if the block button is pressed set the players status to blocking
		remainingBlockFrames = totalBlockFrames;//set the amount of frames they will block to the full amount

	}


	public void onHit ()//when the player hits the enemy
	{
		if (Fader.active==false)//if the comboBar isn't active reset the players multiplier
			comboMultiplier=0;
		comboMultiplier++;//add 1 to the multiplier
		scoreboard.text= "Player Score: " + score;//update the players score
		multiplierDisplay.text = "x" + comboMultiplier;// update the combo multiplier display
		Fader.resetAlpha ();//reset the fading away of the combo bar
	}
}
