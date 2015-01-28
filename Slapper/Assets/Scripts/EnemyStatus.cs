using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyStatus : MonoBehaviour {
	public bool vulnerableLeft;
	public bool vulnerableRight;
	public int enemyhealth;
	Animator anim;
	int quotePicker;
	public Text message;
	public Image speechBubble;
	public GameObject player;
	PlayerStatus playerStat;
	public float timeBetweenEvents=3;
	float timeRemaining;
	int randomNumberHolder;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();//used to set parameters in enemy animation tree
		playerStat = player.GetComponent<PlayerStatus> ();
		timeRemaining = timeBetweenEvents;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeRemaining<=0)
		{
			timeRemaining=timeBetweenEvents;
			anim.SetInteger("AnimationToStart",Random.Range(1,4));

		}
		else
			timeRemaining-=Time.deltaTime;
	
	}
	public void hit()//call when enemy is hit
	{
		enemyhealth--;//lose one health
		if(enemyhealth<=0)
		{
			gameOver();
		}
	}

	public void Talk(){//call during vulnerable animation 
		speechBubble.enabled = true;
		message.enabled = true;
		quotePicker = Random.Range (1, 4);//random quote 
		if(quotePicker==1)
			message.text="DO YOU EVEN LIFT";
		else if(quotePicker==2)
			message.text="COME AT ME BRO";
		else if(quotePicker==3)
			message.text="COOL STORY BRO";

		//make enemy vulnerable
		vulnerableLeft = true;
		vulnerableRight = true;
	}
	public void endTalk(){
		speechBubble.enabled=false;
		message.enabled = false;
		vulnerableLeft = false;
		vulnerableRight = false;
	}
	public void leftAttack()
	{
		if(playerStat.dodgeLeft==false)//if the player isn't dodging
		{
			playerStat.playersHealth--;
			if(playerStat.playersHealth<=0)
			{
				gameOver();
			}
		}
	}
	public void rightAttack()
	{
		if(playerStat.dodgeRight==false)//if the player isn't dodging
		{
			playerStat.playersHealth--;
		}
	}

	public void endAnimation()
	{
		anim.SetInteger ("AnimationToStart", 0);
	}

	public void gameOver(){
	}
}
