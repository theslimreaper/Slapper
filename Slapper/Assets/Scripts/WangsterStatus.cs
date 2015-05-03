using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WangsterStatus : MonoBehaviour {
	[HideInInspector]
	public bool vulnerableLeft;
	[HideInInspector]
	public bool vulnerableRight;
	//the players gameobject
	public GameObject player;

	//finish screen objects
	[Header("Finish Screen items")]
	public Text message;
	public Image resultsBackground;
	public GameObject winAnimation;
	public GameObject loseAnimation;

	//UI
	[Header("UI")]
	public Image resultsSpeech;
	public Sprite winSpeech;
	public Sprite loseSpeech;
	public Image resultsMain;
	public Sprite winResult;
	public Sprite loseResult;
	public Image speechBubble;
	public int enemyhealth;//current health
	public Image EnemyHealthbar;
	//time for attacks to start
	public int maxFramesBetweenEvents=2;
	public int minFramesBetweenEvents=1;
	int framesRemaining;

	//particle system for each and are controlled by these
	[Header("Particle system for hands")]
	public GameObject leftHand;
	ParticleSystem leftSystem;
	public GameObject rightHand;
	ParticleSystem rightSystem;


	[Header("Images to convey hits")]
	public Image[] onHitImages;


	public float maxDelayFinishScreen = 1.0f;
	//textures to swap between in animations and the model that uses the textures
	[Header("Texture Objects")]
	public Texture highNormal;
	public Texture highSpeech;
	public Texture highHit;
	public Texture highAttack;
	public Texture lowNormal;
	public Texture lowSpeech;
	public Texture lowHit;
	public Texture lowAttack;
	public GameObject model;

	bool gameOverCalled=false;
	bool enraged;
	bool needReset=false;
	LightShifter lightsController;
	bool vulnerable;
	int randomNumberHolder;
	float maxEnemyHealth;
	Animator anim;
	PlayerStatusWangster playerStat;
	float gameOverTimer=0.0f;


	public int[] hitPercents;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();//used to set parameters in enemy animation tree
		playerStat = player.GetComponent<PlayerStatusWangster> ();
		framesRemaining = maxFramesBetweenEvents;
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
			vulnerableLeft=false;
			vulnerableRight=false;
			needReset=false;
			framesRemaining=Random.Range (minFramesBetweenEvents,maxFramesBetweenEvents);
		}


		if(gameOverTimer>0)//if the timers been started continue it
			gameOverTimer+=Time.deltaTime;
		if(gameOverTimer>=maxDelayFinishScreen&& !gameOverCalled)
		{
			gameOver (true);
			gameOverCalled=true;
		}
	
	}
	public void hit()//call when enemy is hit
	{
		enemyhealth-= 1;//lose one health
		playerStat.invuln=true;
		playerStat.invulntimer=30;
		if (enemyhealth <= 0)
		{
			anim.SetBool ("NoHealth", true);
			endTalk();
		}

		anim.SetBool("Hit",true);

		if(vulnerable)
		{
			anim.SetBool ("vulnerable",true);
		}
		onHitImages [Random.Range (0, onHitImages.Length)].GetComponent<CanvasGroup> ().alpha = 1;
		LightShifter.hit (enemyhealth/maxEnemyHealth);
		EnemyHealthbar.fillAmount= enemyhealth/ maxEnemyHealth * .68f + .32f;//update health bar

		if(enemyhealth/maxEnemyHealth <.35f)//enrage if below 35% of total health
		{
			anim.SetBool("Enraged",true);
			enraged=true;
			model.renderer.material.mainTexture=lowNormal;
		}

		if (enraged)
			model.renderer.material.mainTexture = lowHit;
		else
			model.renderer.material.mainTexture=highHit;
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

	//hides the speech bubble when the enemy is done talking
	public void endTalk(){
		speechBubble.enabled=false;
		message.enabled = false;
		if(enraged)
			model.renderer.material.mainTexture=lowNormal;
		else
			model.renderer.material.mainTexture=highNormal;
	}

	//called if the left hand is hitting the right side of the player
	public void LeftHandRightAttack()
	{
		if(playerStat.dodgeRight==false&&playerStat.invuln==false)//if the player isn't dodging
		{
			playerStat.playersHealth--;
			playerStat.flinch();
			playerStat.updateRewardBar(false);
			playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth * .66f + .34f;
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

	//called for when the left hand is hitting the left side of the player
	public void LeftHandLeftAttack()
	{
		if(playerStat.dodgeLeft==false&&playerStat.invuln==false)//if the player isn't dodging
		{
			playerStat.playersHealth--;
			playerStat.flinch();
			playerStat.updateRewardBar(false);
			playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth * .66f + .34f;
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

	//called for when the right hand is hitting the left side of the player
	public void RightHandLeftAttack()
	{
		if(playerStat.dodgeLeft==false&&playerStat.invuln==false)//if the player isn't dodging
		{
			playerStat.playersHealth--;
			playerStat.flinch();
			playerStat.updateRewardBar(false);
			playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth * .66f + .34f;
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

	//called for when the right hand of the enemy is hitting the right side of the player
	public void RightHandRightAttack()
	{
		if(playerStat.dodgeRight==false&&playerStat.invuln==false)//if the player isn't dodging
		{
			playerStat.playersHealth--;
			playerStat.flinch();
			playerStat.updateRewardBar(false);
			playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth * .66f + .34f;
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

	//called at the end of the animation to reset to idle
	public void endAnimation()
	{
		anim.SetInteger ("AnimationToStart", 0);
		framesRemaining = Random.Range (minFramesBetweenEvents, maxFramesBetweenEvents);
		if (enraged)
			model.renderer.material.mainTexture = lowNormal;
		else
			model.renderer.material.mainTexture = highNormal;
	}

	//call when the game ends, this brings up the results screen
	public void gameOver(bool playerWon)
	{
		Camera.main.GetComponent<DepthOfFieldScatter> ().enabled = false;
		resultsBackground.gameObject.SetActive (true);
		if(playerWon==true)
		{
			winAnimation.SetActive(true);
			resultsSpeech.sprite=winSpeech;
			resultsMain.sprite=winResult;
			FightChoiceSlider.firstCompleted=true;
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

	//called for when the left hand would hit the player with an undodgeable attack
	public void LeftHandUnblockable()
	{
		if(playerStat.invuln==false)
		{
			playerStat.playersHealth--;
			playerStat.flinch();
			playerStat.updateRewardBar(false);
			playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth * .66f + .34f;
			audio.Play ();
			leftSystem.Emit (5);
			if(playerStat.playersHealth<=0)
			{
				gameOver(false);
			}
			else
				onHitImages [Random.Range (0, onHitImages.Length)].GetComponent<CanvasGroup> ().alpha = 1;
		}
	}

	//called for when the right hand would hit the player with an undodgeable attack
	public void RightHandUnblockable()
	{
		if(playerStat.invuln==false)
		{
			playerStat.playersHealth--;
			playerStat.flinch();
			playerStat.updateRewardBar(false);
			playerStat.playerHealthbar.fillAmount= playerStat.playersHealth/playerStat.maxPlayerHealth * .66f + .34f;
			audio.Play ();
			rightSystem.Emit (5);
			if(playerStat.playersHealth<=0)
			{
				gameOver(false);
			}
			else
				onHitImages [Random.Range (0, onHitImages.Length)].GetComponent<CanvasGroup> ().alpha = 1;
		}
	}

	//used to set the stage where he can be hit into the staggered animations
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
	//resets the animator controllers values to prevent incorrect animations
	public void hitOff()
	{
		anim.SetBool ("Hit", false);
		anim.SetBool ("vulnerable",false);
	}

	//toggle the visibility of the bottle for the drinking


	//switches the texture for when hes about to attack
	public void atackTexture()
	{
		if (enraged)
			model.renderer.material.mainTexture = lowAttack;
		else
			model.renderer.material.mainTexture = highAttack;
	}

	//swtiches the hat thats visible during the knockout animation

	//called at the end of the KO animation, starts a timer that will display the results screen at the end
	public void waitBeforeGameover()
	{
		gameOverTimer = 0.1f;
	}

	public void toggleDrinkingMouth()
	{
		if (model.renderer.material.mainTexture==highNormal)
			model.renderer.material.mainTexture=highHit;
		else if(enemyhealth/maxEnemyHealth>.35)
			model.renderer.material.mainTexture=highNormal;
		else
			model.renderer.material.mainTexture=lowNormal;
	
	}

	public void upAttackFrames()//call at each point where backhand can occur, and count down frames left till attack
	{
		if(framesRemaining<=0)
		{
			needReset=true;
			anim.SetInteger("AnimationToStart",1);
			print ("enemy attacked");
			framesRemaining = Random.Range (minFramesBetweenEvents, maxFramesBetweenEvents);

		}
		else
		{
			framesRemaining-=1;
			print ("frames left: "+framesRemaining);
		}
	}
	public void downAttackFrames()//call at each point where forehand can occur and count down the frames left till attack
	{
		if(framesRemaining<=0)
		{
			needReset=true;
			anim.SetInteger("AnimationToStart",2);
			print ("enemy attacked");
			framesRemaining = Random.Range (minFramesBetweenEvents, maxFramesBetweenEvents);
		}
		else
		{
			framesRemaining-=1;
			print ("frames left: "+framesRemaining);
		}
	}

}
