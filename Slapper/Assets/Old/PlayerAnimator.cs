using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {
	Animator anim;
	int chainNumber=0;
	bool heavyActive = false;
	public float waitDuration=2.0f;
	public Image attackSelectionMenu;//needed to change which move will be used next
	MoveSelector moveselect;// same as above
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();//used to set parameters in animation tree
		moveselect = attackSelectionMenu.GetComponent<MoveSelector>();
	}


	public void lightAttack()//call on button press to start animation
	{
		chainNumber++;//move to next portion of the chain

		if(chainNumber>3)//if the whole chain has been completed return to the first attack
		{
			moveselect.changeLightAttack();//switches the attack
			chainNumber=1;
		}

		anim.SetInteger ("CurrentChainNumber", chainNumber);


	}

	public void heavyAttack()//call on button press to start animation
	{
		heavyActive = true;//start heavy animation
		anim.SetBool("OngoingHeavy",heavyActive);


	}
	public void endHeavy()//call at end of heavy animation to stop looping
	{
		heavyActive = false;//start heavy animation
		anim.SetBool("OngoingHeavy",heavyActive);
		moveselect.changeHeavyAttack ();//pick new ability
	}

	public void breakChain()
	{
	}

}
