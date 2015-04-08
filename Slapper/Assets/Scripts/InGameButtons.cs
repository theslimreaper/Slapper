using UnityEngine;
using System.Collections;

public class InGameButtons : MonoBehaviour {
	public GameObject enemy;
	EnemyStatus enemyStat;
	// Use this for initialization
	void Start () {
		enemyStat = enemy.GetComponent<EnemyStatus> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void nextButtonPress(string nextLevel)//calls next level if the enemyshealth is zero otherwise restart the fight
	{
		if (enemyStat.enemyhealth == 0) {
						Application.LoadLevel (nextLevel);
				} else
						Application.LoadLevel (Application.loadedLevel);

	}
}
