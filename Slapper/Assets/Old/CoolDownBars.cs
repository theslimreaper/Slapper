using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoolDownBars : MonoBehaviour {
	//the cooldown bars
	public Image superCooldown;
	public Button superButton;
	public Image deflectCooldown;
	public Button deflectButton;
	public Image lightCooldown;
	public Button lightButton;
	public Image heavyCooldown;
	public Button heavyButton;
	//how many frames it takes to reset
	public int lightFramesDown;
	public int heavyFramesDown;
	public int deflectFramesDown;
	int lightCounter=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {//if the cooldown bar is up slowly shrink it and keep button uninteractable, once its gone make buttons interactable
		if(lightCooldown.fillAmount>0)
		{
			lightCooldown.fillAmount=lightCooldown.fillAmount-(1.0f/ lightFramesDown);
			lightButton.interactable=false;
		}
		else
			lightButton.interactable=true;
		if(heavyCooldown.fillAmount>0)
		{
			heavyCooldown.fillAmount=heavyCooldown.fillAmount-(1.0f/ heavyFramesDown);
			heavyButton.interactable=false;
		}
		else
			heavyButton.interactable=true;
		if(deflectCooldown.fillAmount>0)
		{
			deflectCooldown.fillAmount=deflectCooldown.fillAmount-(1.0f/ deflectFramesDown);
			deflectButton.interactable=false;
		}
		else
			deflectButton.interactable=true;

	}

	public void lightCooldownActivate()//after 3 hits go on cooldown
	{
		lightCounter++;
		if(lightCooldown.fillAmount<=0&&lightCounter>=3)
		{
			lightCooldown.fillAmount = 1;
			lightCounter=0;
		}
	}

	public void heavyCooldownActivate()
	{
		if(heavyCooldown.fillAmount<=0)
			heavyCooldown.fillAmount = 1;
	}

	public void deflectCooldownActivate()
	{
		if(deflectCooldown.fillAmount<=0)
			deflectCooldown.fillAmount = 1;
	}
	public void SupperToggle()
	{
		superCooldown.fillAmount = 1;
	}

}
