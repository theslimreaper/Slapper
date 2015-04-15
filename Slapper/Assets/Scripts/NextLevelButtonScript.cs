using UnityEngine;
using System.Collections;

public class NextLevelButtonScript : MonoBehaviour {
	public EnemyStatus nextLevelChecker;
	public string failedNext;
	public string succeededNext;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		}


	public void nextLevelLoader(){
		if (nextLevelChecker.enemyhealth <= 0)
			Application.LoadLevel (succeededNext);
		else
			Application.LoadLevel (failedNext);
	}


}
