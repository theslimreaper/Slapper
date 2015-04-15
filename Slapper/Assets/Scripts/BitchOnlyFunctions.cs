using UnityEngine;
using System.Collections;

public class BitchOnlyFunctions : MonoBehaviour {

	public GameObject phone;
	public Light cameraLight;
	// Use this for initialization
	void Start () {
	
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
			cameraLight.enabled=false;
		else
			cameraLight.enabled=true;
	}
}

