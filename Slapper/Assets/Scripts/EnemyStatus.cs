using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyStatus : MonoBehaviour {
	public bool vulnerableLeft;
	public bool vulnerableRight;
	public int enemyhealth;
	float maxEnemyHealth;
	Animator anim;
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
	public GameObject leftHand;
	public GameObject rightHand;
	ParticleSystem leftSystem;
	ParticleSystem rightSystem;
	bool needReset=false;
	LightShifter lightsController;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();//used to set parameters in enemy animation tree
		playerStat = player.GetComponent<PlayerStatus> ();
		timeRemaining = timeBetweenEvents;
		resultsBackground.gameObject.SetActive (false);
		maxEnemyHealth = enemyhealth;
		leftSystem = leftHand.GetComponent<ParticleSystem> ();
		rightSystem = rightHand.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")&&needReset==true)
		{
//			print ("returned");
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
			if(randomNumberHolder<30)
				anim.SetInteger("AnimationToStart",1);
			else if(randomNumberHolder<60)
				anim.SetInteger("AnimationToStart",2);
			else if(randomNumberHolder<=100)
				anim.SetInteger("AnimationToStart",3);
//			print ("new move go");
		}
		else
			timeRemaining-=Time.deltaTime;


	
	}
	public void hit(int damage)//call when enemy is hit
	{
	//	print ("hit");
		enemyhealth-= damage;//lose one health
		EnemyHealthbar.fillAmount= enemyhealth/ maxEnemyHealth;//update health bar
		LightShifter.hit (enemyhealth/maxEnemyHealth);
		anim.SetBool("Hit",true);
		if(enemyhealth/maxEnemyHealth <.35f)//enrage if below 35% of total health
			anim.SetBool("Enraged",true);
		if(enemyhealth<=0)//when he dies go to the gameover with a success
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
	public void swapSlapTalk()
	{
		speechBubble.enabled = true;
		message.enabled = true;
		message.text="YOU READY FOR THIS?";
	}
	public void slapClapTalk()
	{
		speechBubble.enabled = true;
		message.enabled = true;
		message.text="WASSUP";
	}
	public void youBastardTalk()
	{
		speechBubble.enabled = true;
		message.enabled = true;
		message.text="COME ON";
	}

	public void LeftHandRightAttack()//four different ways the normal attacks would be delivered
	{
		if(playerStat.dodgeRight==false)//if the player isn't dodging
		{
			playerStat.playersHealth--;
			playerStat.updateRewardBar(false);
			playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth;
			audio.Play ();
			leftSystem.Emit (5);
			if(playerStat.playersHealth<=0)
			{
				gameOver(false);
			}
		}
	}
	public void LeftHandLeftAttack()
	{
		if(playerStat.dodgeLeft==false)//if the player isn't dodging
		{
			playerStat.playersHealth--;
			playerStat.updateRewardBar(false);
			playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth;
			audio.Play ();
			leftSystem.Emit (5);
			if(playerStat.playersHealth<=0)
			{
				gameOver(false);
			}
		}
	}
	public void RightHandLeftAttack()
	{
		if(playerStat.dodgeLeft==false)//if the player isn't dodging
		{
			playerStat.playersHealth--;
			playerStat.updateRewardBar(false);
			playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth;
			audio.Play ();
			rightSystem.Emit (5);
			if(playerStat.playersHealth<=0)
			{
				gameOver(false);
			}
		}
	}
	public void RightHandRightAttack()
	{
		if(playerStat.dodgeRight==false)//if the player isn't dodging
		{
			playerStat.playersHealth--;
			playerStat.updateRewardBar(false);
			playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth;
			audio.Play ();
			leftSystem.Emit (5);
			if(playerStat.playersHealth<=0)
			{
				gameOver(false);
			}
		}
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
	//	print ("left open");
	}

	public void leftSafe()//call when the left side can no longer be hit
	{
		vulnerableLeft = false;
	//	print ("left safe");
	}
	public void rightOpen()//call when the right side can be hit
	{
		vulnerableRight = true;
	//	print ("right open");
	}
	public void rightSafe()//call when the right side can no longer be hit
	{
		vulnerableRight = false;
	//	print ("right safe");
	}

	public void LeftHandUnblockable()
	{
		playerStat.playersHealth--;
		playerStat.updateRewardBar(false);
		playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth;
		audio.Play ();
		leftSystem.Emit (5);
		if(playerStat.playersHealth<=0)
		{
			gameOver(false);
		}
	}
	public void RightHandUnblockable()
	{
		playerStat.updateRewardBar(false);
		playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth;
		audio.Play ();
		rightSystem.Emit (5);
		if(playerStat.playersHealth<=0)
		{
			gameOver(false);
		}
	}

	public void hitOff()
	{
		anim.SetBool ("Hit", false);
	}
}
