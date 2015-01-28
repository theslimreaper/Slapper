using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	public int playersHealth;
	public bool dodgeLeft=false;
	public bool dodgeRight=false;
	public Image playerHealthbar;
	public GameObject enemy;
	EnemyStatus enemyStat;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();//used to set parameters in players animation tree
		enemyStat = enemy.GetComponent<EnemyStatus> ();
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

		//release the dodge
		if(Input.GetKeyUp(KeyCode.Z))
			anim.SetBool("DodgeLeft", false);
		if(Input.GetKeyUp(KeyCode.M))
			anim.SetBool("DodgeRight",false);
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
		print ("start left dodge");
		dodgeLeft = true;
	}
	public void startRightDodge()
	{
		print ("start right dodge");
		dodgeRight = true;
	}

	public void endLeftDodge()
	{
		anim.SetBool ("DodgeLeft", false);
		dodgeLeft = false;
	}

	public void endRightDodge()
	{
		print ("end right dodge");
		anim.SetBool ("DodgeRight", false);
		dodgeRight = false;
	}


	public void leftHit(){//call during frame where your attack connects from the left
		if(enemyStat.vulnerableLeft==true)
		{
			enemyStat.hit();
			if(enemyStat.enemyhealth<=0)
			{

			}
		}
	}
	public void rightHit(){//call during frame where your attack connects from the right
		if(enemyStat.vulnerableRight==true)
		{
			enemyStat.hit();
			if(enemyStat.enemyhealth<=0)
			{

			}
		}
	}


}
