using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlsSetup : MonoBehaviour {
	public Sprite TapSelected;
	public Sprite SwipeSelected;
	public Sprite TiltSelected;
	public Image dodgeImage;
	public Image attackImage;
	public static int attackChoice=2;//start as tap
	public static int dodgeChoice=1;//start as tilt
	int temp;
	void Start(){
		if (attackChoice == 1)
			attackImage.sprite = TiltSelected;
		if (attackChoice == 2)
			attackImage.sprite=TapSelected;
		if (attackChoice == 3)
			attackImage.sprite=SwipeSelected;
		if(dodgeChoice==1)
			dodgeImage.sprite=TiltSelected;
		if(dodgeChoice==2)
			dodgeImage.sprite=TapSelected;
		if(dodgeChoice==3)
			dodgeImage.sprite=SwipeSelected;

	}
	public void dodgeTilt(){
		if(attackChoice!=1)
		{
			dodgeChoice=1;
			dodgeImage.sprite=TiltSelected;
		}
		else
		{
			temp=dodgeChoice;//swap attack and dodge
			attackChoice=temp;
			dodgeChoice=1;
			dodgeImage.sprite=TiltSelected;
			if(attackChoice==2)
				attackImage.sprite=TapSelected;
			else if(attackChoice==3)
				attackImage.sprite=SwipeSelected;


		}

	}


	public void dodgeTap(){
		if(attackChoice!=2)
		{
			dodgeChoice=2;
			dodgeImage.sprite=TapSelected;
	
		}
		else
		{
			temp=dodgeChoice;//swap attack and dodge
			attackChoice=temp;
			dodgeChoice=2;
			dodgeImage.sprite=TapSelected;
			if(attackChoice==1)
				attackImage.sprite=TiltSelected;
			else if(attackChoice==3)
				attackImage.sprite=SwipeSelected;
			
		}
	}



	public void dodgeSwipe(){
		if(attackChoice!=3)
		{
			dodgeChoice=3;
			dodgeImage.sprite=SwipeSelected;

		}
		else 
		{
			temp=dodgeChoice;//swap attack and dodge
			attackChoice=temp;
			dodgeChoice=3;
			dodgeImage.sprite=SwipeSelected;
			if(attackChoice==1)
				attackImage.sprite=TiltSelected;
			else if(attackChoice==2)
				attackImage.sprite=TapSelected;
			
		}
		
	}



	public void attackTilt(){
		if(dodgeChoice!=1)
		{
			attackChoice=1;
			attackImage.sprite=TiltSelected;
		}
		else 
		{
			temp=attackChoice;
			dodgeChoice=temp;
			attackChoice=1;
			attackImage.sprite=TiltSelected;
			if(dodgeChoice==2)
				dodgeImage.sprite=TapSelected;
			else if (dodgeChoice==3)
				dodgeImage.sprite=SwipeSelected;
		}

	}
	public void attackTap(){
		if(dodgeChoice!=2)
		{
			attackChoice=2;
			attackImage.sprite=TapSelected;

		}
		else
		{
			temp=attackChoice;
			dodgeChoice=temp;
			attackChoice=2;
			attackImage.sprite=TapSelected;
			if(dodgeChoice==1)
				dodgeImage.sprite=TiltSelected;
			else if (dodgeChoice==3)
				dodgeImage.sprite=SwipeSelected;
		}
		
	}
	public void attackSwipe(){
		if(dodgeChoice!=3)
		{
			attackChoice=3;
			attackImage.sprite=SwipeSelected;
		}
		else
		{
			temp=attackChoice;
			dodgeChoice=temp;
			attackChoice=3;
			attackImage.sprite=SwipeSelected;
			if(dodgeChoice==1)
				dodgeImage.sprite=TiltSelected;
			else if (dodgeChoice==2)
				dodgeImage.sprite=TapSelected;
		}
		
	}
}
