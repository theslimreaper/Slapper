using UnityEngine;
using System.Collections;

public class BitchOnlyFunctions : MonoBehaviour {

	public GameObject phone;
	public Light cameraLight;
	Animator anim;
	public Texture duckFace;
	Texture baseTexture;
	public bool leftCounterUp;
	public bool rightCounterUp;
	public GameObject model;
	// Use this for initialization
	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		baseTexture = model.renderer.material.mainTexture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void togglePhone()
	{
		if(phone.activeSelf)
			phone.SetActive(false);
		else
			phone.SetActive(true);
	}

	public void flashCamera()
	{
		if(cameraLight.enabled==true)
		{
			cameraLight.enabled=false;
			model.renderer.material.mainTexture=baseTexture;
		}
		else
		{
			cameraLight.enabled=true;
			model.renderer.material.mainTexture=duckFace;
		}
	}
	
	public void leftCounterEnabled()
	{
		leftCounterUp = true;
	}
	public void leftCounterDisable()
	{
		leftCounterUp = false;
	}
	
	public void rightCounterEnabled()
	{
		rightCounterUp = true;
	}
	public void rightCounterDisable()
	{
		rightCounterUp = false;	
	}
	public void leftCounterActivate()
	{
		anim.SetInteger ("Counter", -1);
	}
	public void rightCounterActivate()
	{
		anim.SetInteger ("Counter", 1);
	}
	public void endCounters()
	{
		anim.SetInteger ("Counter", 0);

	}
}

