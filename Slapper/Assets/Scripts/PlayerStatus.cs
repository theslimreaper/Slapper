﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	public int playersHealth;
	public bool dodgeLeft=false;
	public bool dodgeRight=false;
	public Image playerHealthbar;
	public GameObject enemy;
	public GameObject leftWrist;//used for particle emission
	public GameObject rightWrist;
	public ControlsSetup controls;
	private Vector2 fp=new Vector2();
	private Vector2 lp=new Vector2();
	public float swipeSens = 50f;
	public bool rewardOn=true;
	public Image rewardBar;
	public Text rewardText;
	[HideInInspector]
	public int rewardCounter=0;
	[HideInInspector]
	public float maxPlayerHealth;
	EnemyStatus enemyStat;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();//used to set parameters in players animation tree
		enemyStat = enemy.GetComponent<EnemyStatus> ();
		AudioListener.volume = MenuFunctions.volumeLevel;
	//	print (MenuFunctions.volumeLevel);
		Time.timeScale = MenuFunctions.gameSpeed;
	//	print (MenuFunctions.gameSpeed);
		maxPlayerHealth = playersHealth;
	}

	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.WindowsPlayer||Application.platform==RuntimePlatform.WindowsEditor)
		{
			if(Input.GetKeyDown(KeyCode.Z)&&anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			   beginLeftDodgeAnimation();
			if(Input.GetKeyDown(KeyCode.X))
				beginLeftAttackAnimation();
			if(Input.GetKeyDown(KeyCode.N))
				beginRightAttackAnimation();
			if(Input.GetKeyDown(KeyCode.M)&&anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
				beginRightDodgeAnimation();
		//release the dodge
			if(Input.GetKeyUp(KeyCode.Z))
				anim.SetBool("DodgeLeft", false);
			if(Input.GetKeyUp(KeyCode.M))
				anim.SetBool("DodgeRight",false);
		}
		if (Application.platform == RuntimePlatform.Android&&MenuFunctions.accelerometer==true)
		{
			if(controls.dodgeChoice==1)//dodge tilt controls
			{
				Vector3 currentDir;
				currentDir.x = Input.acceleration.x;
				if(currentDir.x<-0.25 &&anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
					beginLeftDodgeAnimation();
				if(currentDir.x>0.25&&anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
					beginRightDodgeAnimation();
				if(currentDir.x>-0.25)
					anim.SetBool("DodgeLeft", false);
				if(currentDir.x<0.25)
					anim.SetBool("DodgeRight",false);   
			}
			else if(controls.dodgeChoice==3)//dodge swipe controls
			{
				//Touch touch = Input.touches[0];

				foreach(Touch touch in Input.touches)
				{
					if(touch.phase==TouchPhase.Began)
					{
						fp=touch.position;
						lp=touch.position;
					}
					if(touch.phase==TouchPhase.Moved)
					{
						lp=touch.position;
					}
					if(touch.phase==TouchPhase.Ended)
					{
						if(fp.x-lp.x>swipeSens)//left swipe
						{
							beginLeftDodgeAnimation();
						}
						else if(fp.x-lp.x<-swipeSens)//right swipe
						{
							beginRightDodgeAnimation();
						}
					}
				}
			}
			if(controls.attackChoice==1)//attack tilt
			{
				Vector3 currentDir;
				currentDir.x = Input.acceleration.x;
				if(currentDir.x<-0.25 &&anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
					beginLeftAttackAnimation();
				if(currentDir.x>0.25&&anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
					beginRightAttackAnimation();
			}
			else if(controls.attackChoice==3)//attack swipe
			{
				//Touch touch = Input.touches[0];
				//Vector2 fp=new Vector2();
				//Vector2 lp=new Vector2();
				foreach(Touch touch in Input.touches)
				{
					if(touch.phase==TouchPhase.Began)
					{
						fp=touch.position;
						lp=touch.position;
					}
					if(touch.phase==TouchPhase.Moved)
					{
						lp=touch.position;
					}
					if(touch.phase==TouchPhase.Ended)
					{
						if(fp.x-lp.x>swipeSens)//left swipe
						{
							if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
								beginRightAttackAnimation();
						}
						else if(fp.x-lp.x<-swipeSens)//right swipe
						{
							if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
								beginLeftAttackAnimation();
						}
					}
				}
			}
		}


	}

	public void leftAttackButton(){
		if(controls.attackChoice==2)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
				beginLeftAttackAnimation();
		}
	}
	public void rightAttackButton(){
		if(controls.attackChoice==2)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
				beginRightAttackAnimation();
		}
	}
	public void leftDodgeButton(){
		if(controls.dodgeChoice==2)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
				beginLeftDodgeAnimation();
		}
	}
	public void rightDodgeButton(){
		if(controls.dodgeChoice==2)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
				beginRightDodgeAnimation();
		}
	}
	/*********************************
	 * FUNCTIONS CALLED FROM KEY PRESS
	 * ********************************
	 * */
	public void beginLeftAttackAnimation()
	{
		anim.SetBool ("LeftAttack", true);
	}

	public void beginRightAttackAnimation()
	{
		anim.SetBool ("RightAttack", true);
	}

	public void beginLeftDodgeAnimation()
	{
		anim.SetBool ("DodgeLeft", true);
	}
	public void beginRightDodgeAnimation()
	{
		anim.SetBool ("DodgeRight", true);
	}

	public void endLeftAttack()//call at end of attack animation to return to neutral position
	{
		anim.SetBool ("LeftAttack", false);
	}

	public void endRightAttack()//call at end of attack animation to return to neutral position
	{
		anim.SetBool ("RightAttack", false);
	}


	/*********************************
	 * FUNCTIONS CALLED DURING ANIMATIONS
	 * ********************************
	 * */

	public void startLeftDodge()
	{
		dodgeLeft = true;
	}
	public void startRightDodge()
	{
		dodgeRight = true;
	}

	public void endLeftDodge()
	{
		anim.SetBool ("DodgeLeft", false);
		dodgeLeft = false;
	}

	public void endRightDodge()
	{
		anim.SetBool ("DodgeRight", false);
		dodgeRight = false;
	}


	public void leftHit(){//call during frame where your attack connects from the left
		//print ("left attack, vulnerable: " + enemyStat.vulnerableLeft);
		if (enemyStat.vulnerableLeft == true) {
						updateRewardBar (true);
						if (rewardCounter < 5)//if 5 dodges without being hit double the damage
								enemyStat.hit (1, true);
						else
								enemyStat.hit (2, true);
						audio.Play ();
						leftWrist.particleSystem.Emit (5);	
				} 
		else
		{
			enemyStat.hit (1, false);//if the enemy isnt vulnerable do small damage
			audio.Play ();
			leftWrist.particleSystem.Emit (2);
		}
	}
	public void rightHit(){//call during frame where your attack connects from the right
	//	print ("right attack, vulnerable: " + enemyStat.vulnerableRight);
		if(enemyStat.vulnerableRight==true)
		{
			updateRewardBar(true);
			if(rewardCounter<5)//if 5 dodges without being hit double the damage
				enemyStat.hit(1,true);
			else
				enemyStat.hit (2,true);
			audio.Play();
			rightWrist.particleSystem.Emit (5);
		}
		else
		{
			enemyStat.hit (1,false);
			audio.Play();
			rightWrist.particleSystem.Emit (2);
		}
	}
	public void updateRewardBar(bool plus)
	{
		if (plus==true)
		{
			if(rewardCounter<5)
			{
				rewardCounter++;
				rewardBar.fillAmount+= .2f;
			}
			if(rewardCounter>=5)
			{
				rewardText.gameObject.SetActive(true);
			}

		}
		else
		{
			rewardBar.fillAmount=0.0f;
			rewardCounter=0;
			rewardText.gameObject.SetActive(false);

		}
	}
}
