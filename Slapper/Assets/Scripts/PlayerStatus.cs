using UnityEngine;
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
	EnemyStatus enemyStat;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();//used to set parameters in players animation tree
		enemyStat = enemy.GetComponent<EnemyStatus> ();
		AudioListener.volume = MenuFunctions.volumeLevel;
		print (MenuFunctions.volumeLevel);
		Time.timeScale = MenuFunctions.gameSpeed;
		print (MenuFunctions.gameSpeed);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Z)&&anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
		   beginLeftDodgeAnimation();
		if(Input.GetKeyDown(KeyCode.X)&&anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			beginLeftAttackAnimation();
		if(Input.GetKeyDown(KeyCode.N)&&anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			beginRightAttackAnimation();
		if(Input.GetKeyDown(KeyCode.M)&&anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			beginRightDodgeAnimation();

		Vector3 currentDir;
		currentDir.x = Input.acceleration.x;
		if(currentDir.x<-0.25 &&anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			beginLeftDodgeAnimation();
		if(currentDir.x>0.25&&anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			beginRightDodgeAnimation();     
		//release the dodge
		if(Input.GetKeyUp(KeyCode.Z))
			anim.SetBool("DodgeLeft", false);
		if(Input.GetKeyUp(KeyCode.M))
			anim.SetBool("DodgeRight",false);

		if(currentDir.x>-0.25)
			anim.SetBool("DodgeLeft", false);
		if(currentDir.x<0.25)
			anim.SetBool("DodgeRight",false);   
	}

	public void leftAttackButton(){
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			beginLeftAttackAnimation();
	}
	public void rightAttackButton(){
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			beginRightAttackAnimation();
	}
	public void leftDodgeButton(){
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			beginLeftDodgeAnimation();
	}
	public void rightDodgeButton(){
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			beginRightDodgeAnimation();
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
		dodgeLeft = false;
	}

	public void endRightDodge()
	{
		anim.SetBool ("DodgeRight", false);
		dodgeRight = false;
	}


	public void leftHit(){//call during frame where your attack connects from the left
		print ("left attack, vulnerable: " + enemyStat.vulnerableLeft);
		if(enemyStat.vulnerableLeft==true)
		{
			enemyStat.hit();
			audio.Play();
			leftWrist.particleSystem.Emit(5);
		}
	}
	public void rightHit(){//call during frame where your attack connects from the right
		print ("right attack, vulnerable: " + enemyStat.vulnerableRight);
		if(enemyStat.vulnerableRight==true)
		{
			enemyStat.hit();
			audio.Play();
			rightWrist.particleSystem.Emit (5);
		}
	}


}
