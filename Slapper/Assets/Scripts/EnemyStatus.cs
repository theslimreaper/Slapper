using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyStatus : MonoBehaviour {
	public bool vulnerableLeft;
	public bool vulnerableRight;
	public int enemyhealth;
	float maxEnemyHealth;
	Animator anim;
	int quotePicker;
	public Text message;
	public Text results;
	public Image resultsBackground;
	public Image speechBubble;
	public GameObject player;
	PlayerStatus playerStat;
	public float timeBetweenEvents=3;
	float timeRemaining;
	int randomNumberHolder;
	public Image EnemyHealthbar;
	bool needReset=false;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();//used to set parameters in enemy animation tree
		playerStat = player.GetComponent<PlayerStatus> ();
		timeRemaining = timeBetweenEvents;
		resultsBackground.gameObject.SetActive (false);
		maxEnemyHealth = enemyhealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")&&needReset==true)
		{
			print ("returned");
			anim.SetBool ("Hit", false);
			anim.SetInteger("AnimationToStart",0);
			vulnerableLeft=false;
			vulnerableRight=false;
			needReset=false;
			timeRemaining=timeBetweenEvents;
		}
		if(timeRemaining<=0)
		{
			needReset=true;
			randomNumberHolder=Random.Range(1,100);
			if(randomNumberHolder<25)
				anim.SetInteger("AnimationToStart",1);
			else if(randomNumberHolder<50)
				anim.SetInteger("AnimationToStart",3);
			else if(randomNumberHolder<70)
				anim.SetInteger("AnimationToStart",4);
			else if(randomNumberHolder<100)
				anim.SetInteger("AnimationToStart",2);
			print ("new move go");
		}
		else
			timeRemaining-=Time.deltaTime;

	
	}
	public void hit(int damage)//call when enemy is hit
	{
		print ("hit");
		enemyhealth-= damage;//lose one health
		EnemyHealthbar.fillAmount= enemyhealth/ maxEnemyHealth;
		anim.SetBool("Hit",true);
		if(enemyhealth<=0)
		{
			gameOver(true);
		}
	}

	public void FlexTalk(){//call during vulnerable animation 
		speechBubble.enabled = true;
		message.enabled = true;
		message.text="CHECK ME OUT";
	}
	public void tauntTalk()
	{
		speechBubble.enabled = true;
		message.enabled = true;
		message.text="COME AT ME";
	}
	public void slapTalk()
	{
		speechBubble.enabled = true;
		message.enabled = true;
		message.text="WASSUP";
	}
	public void endTalk(){
		speechBubble.enabled=false;
		message.enabled = false;
	}
	public void leftAttack()
	{
		if(playerStat.dodgeLeft==false)//if the player isn't dodging
		{
			playerStat.playersHealth--;
			playerStat.updateRewardBar(false);
			playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth;
			audio.Play();
			if(playerStat.playersHealth<=0)
			{
				gameOver(false);
			}
		}
		else
			playerStat.updateRewardBar(true);
	}
	public void rightAttack()
	{
		if(playerStat.dodgeRight==false)//if the player isn't dodging
		{
			playerStat.playersHealth--;
			playerStat.updateRewardBar(false);
			playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth;
			audio.Play ();
			if(playerStat.playersHealth<=0)
			{
				gameOver(false);
			}
		}
		else
			playerStat.updateRewardBar(true);
	}

	public void endAnimation()
	{
		anim.SetInteger ("AnimationToStart", 0);
		timeRemaining=timeBetweenEvents;
	}

	public void gameOver(bool playerWon)
	{
		resultsBackground.gameObject.SetActive (true);
		Time.timeScale = 0;
		if(playerWon==true)
		{
			results.text="YOU!";
		}
		else
		{
			results.text="The Brobot";
		}
	}

	public void leftOpen()//call when the left side can be hit
	{
		vulnerableLeft = true;
		print ("left open");
	}

	public void leftSafe()//call when the left side can no longer be hit
	{
		vulnerableLeft = false;
		print ("left safe");
	}
	public void rightOpen()//call when the right side can be hit
	{
		vulnerableRight = true;
		print ("right open");
	}
	public void rightSafe()//call when the right side can no longer be hit
	{
		vulnerableRight = false;
		print ("right safe");
	}
	public void unblockableAttacks()
	{
		playerStat.playersHealth--;
		playerStat.updateRewardBar(false);
		playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth;
		audio.Play ();
		if(playerStat.playersHealth<=0)
		{
			gameOver(false);
		}
	}
}
