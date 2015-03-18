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

	public Image resultsBackground;
	public GameObject winAnimation;
	public GameObject loseAnimation;
	public Image resultsSpeech;
	public Sprite winSpeech;
	public Sprite loseSpeech;
	public Image resultsMain;
	public Sprite winResult;
	public Sprite loseResult;
	public Image speechBubble;
	public GameObject player;
	PlayerStatus playerStat;
	public float maxTimeBetweenEvents=2;
	public float minTimeBetweenEvents=1;
	float timeRemaining;
	int randomNumberHolder;
	public Image EnemyHealthbar;
	public GameObject leftHand;
	public GameObject rightHand;
	ParticleSystem leftSystem;
	ParticleSystem rightSystem;
	bool needReset=false;
	LightShifter lightsController;
	bool vulnerable;
	public Image[] onHitImages;
	public GameObject bottle;
	bool enraged;


	
	
	public Texture highNormal;
	public Texture highSpeech;
	public Texture lowNormal;
	public Texture lowSpeech;
	public GameObject model;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();//used to set parameters in enemy animation tree
		playerStat = player.GetComponent<PlayerStatus> ();
		timeRemaining = maxTimeBetweenEvents;
		resultsBackground.gameObject.SetActive (false);
		maxEnemyHealth = enemyhealth;
		leftSystem = leftHand.GetComponent<ParticleSystem> ();
		rightSystem = rightHand.GetComponent<ParticleSystem> ();
		enraged = false;
	}
	
	// Update is called once per frame
	void Update () {
		if((anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")||anim.GetCurrentAnimatorStateInfo(0).IsName("Enraged Idle"))&&needReset==true)
		{
//			print ("returned");
			anim.SetBool ("Hit", false);
			anim.SetInteger("AnimationToStart",0);
			vulnerableLeft=false;
			vulnerableRight=false;
			needReset=false;
			timeRemaining=Random.Range (minTimeBetweenEvents,maxTimeBetweenEvents);
		}

		if(timeRemaining<=0)
		{
			needReset=true;
			randomNumberHolder=Random.Range(1,100);
			if(randomNumberHolder<20)
				anim.SetInteger("AnimationToStart",1);
			else if(randomNumberHolder<40)
				anim.SetInteger("AnimationToStart",2);
			else if(randomNumberHolder<60)
				anim.SetInteger("AnimationToStart",3);
			else if(randomNumberHolder<80)
				anim.SetInteger("AnimationToStart",4);
			else if(randomNumberHolder<=100)
				anim.SetInteger("AnimationToStart",5);
		}
		else
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")||anim.GetCurrentAnimatorStateInfo(0).IsName("Enraged Idle"))
		{
			timeRemaining-=Time.deltaTime;
		}

	
	}
	public void hit(int damage)//call when enemy is hit
	{
			enemyhealth-= damage;//lose one health
			anim.SetBool("Hit",true);
		if(vulnerable)
		{
			anim.SetBool ("vulnerable",true);
		}
		onHitImages [Random.Range (0, onHitImages.Length)].GetComponent<CanvasGroup> ().alpha = 1;
		LightShifter.hit (enemyhealth/maxEnemyHealth);
		EnemyHealthbar.fillAmount= enemyhealth/ maxEnemyHealth;//update health bar
		if(enemyhealth/maxEnemyHealth <.35f)//enrage if below 35% of total health
		{
			anim.SetBool("Enraged",true);
			enraged=true;
			model.renderer.material.mainTexture=lowNormal;
		}
		if(enemyhealth<=0)//when he dies go to the gameover with a success
		{
			gameOver(true);
		}
	}

	public void FlexTalk(){//call during vulnerable animation 
		speechBubble.enabled = true;
		message.enabled = true;
		message.text="CHECK ME OUT";
		if(enraged)
			model.renderer.material.mainTexture=lowSpeech;
		else
			model.renderer.material.mainTexture=highSpeech;
	}
	public void tauntTalk()
	{
		speechBubble.enabled = true;
		message.enabled = true;
		message.text="COME AT ME";
		if(enraged)
			model.renderer.material.mainTexture=lowSpeech;
		else
			model.renderer.material.mainTexture=highSpeech;
	}
	public void slapTalk()
	{
		speechBubble.enabled = true;
		message.enabled = true;
		message.text="WASSUP";
		if(enraged)
			model.renderer.material.mainTexture=lowSpeech;
		else
			model.renderer.material.mainTexture=highSpeech;
	}

	public void swapSlapTalk()
	{
		speechBubble.enabled = true;
		message.enabled = true;
		message.text="YOU READY FOR THIS?";
		if(enraged)
			model.renderer.material.mainTexture=lowSpeech;
		else
			model.renderer.material.mainTexture=highSpeech;
	}
	public void slapClapTalk()
	{
		speechBubble.enabled = true;
		message.enabled = true;
		message.text="WHATCHA GON DO BOUT IT";
		if(enraged)
			model.renderer.material.mainTexture=lowSpeech;
		else
			model.renderer.material.mainTexture=highSpeech;
	}
	public void youBastardTalk()
	{
		speechBubble.enabled = true;
		message.enabled = true;
		message.text="aww look at ya";
		if(enraged)
			model.renderer.material.mainTexture=lowSpeech;
		else
			model.renderer.material.mainTexture=highSpeech;
	}
	public void endTalk(){
		speechBubble.enabled=false;
		message.enabled = false;
		if(enraged)
			model.renderer.material.mainTexture=lowNormal;
		else
			model.renderer.material.mainTexture=highNormal;
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
			else
				onHitImages [Random.Range (0, onHitImages.Length)].GetComponent<CanvasGroup> ().alpha = 1;
		}
		playerStat.updateRewardBar(true);
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
			else
				onHitImages [Random.Range (0, onHitImages.Length)].GetComponent<CanvasGroup> ().alpha = 1;
		}
		else
			playerStat.updateRewardBar(true);
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
			else
				onHitImages [Random.Range (0, onHitImages.Length)].GetComponent<CanvasGroup> ().alpha = 1;
		}
		else
			playerStat.updateRewardBar(true);
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
			else
				onHitImages [Random.Range (0, onHitImages.Length)].GetComponent<CanvasGroup> ().alpha = 1;
		}
		else
			playerStat.updateRewardBar(true);
	}
	public void endAnimation()
	{
		anim.SetInteger ("AnimationToStart", 0);
		timeRemaining = Random.Range (minTimeBetweenEvents, maxTimeBetweenEvents);
	}

	public void gameOver(bool playerWon)
	{
		resultsBackground.gameObject.SetActive (true);
		if(playerWon==true)
		{
			winAnimation.SetActive(true);
			resultsSpeech.sprite=winSpeech;
			resultsMain.sprite=winResult;
		}
		else
		{
			loseAnimation.SetActive(true);
			resultsSpeech.sprite=loseSpeech;
			resultsMain.sprite=loseResult;
		}
		player.SetActive (false);
		gameObject.SetActive (false);
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
		else
			onHitImages [Random.Range (0, onHitImages.Length)].GetComponent<CanvasGroup> ().alpha = 1;
	}
	public void RightHandUnblockable()
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
		else
			onHitImages [Random.Range (0, onHitImages.Length)].GetComponent<CanvasGroup> ().alpha = 1;
	}
	public void toggleVulnerable()
	{
		if(vulnerable==true)
		{
			vulnerable=false;

		}
		else
		{
			vulnerable=true;

		}
//		print ("vulnerable" + vulnerable);
	}
	public void hitOff()
	{
		anim.SetBool ("Hit", false);
		anim.SetBool ("vulnerable",false);
	}

	public void toggleBottle(){
		if (bottle.activeSelf)
			bottle.SetActive (false);
		else
			bottle.SetActive(true);
	}
}
