using UnityEngine;
using System.Collections;

public class BroOnlyFunctions : MonoBehaviour {
	
	public GameObject bottle;
	public GameObject hatHead;
	public GameObject hatFloor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void hatSwitch()
	{
		hatFloor.gameObject.SetActive (true);
		hatHead.gameObject.SetActive (false);
	}
	public void toggleBottle(){
		if (bottle.activeSelf)
			bottle.SetActive (false);
		else
			bottle.SetActive(true);
	}
}
