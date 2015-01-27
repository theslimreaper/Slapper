using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour {
	public bool vulnerableLeft;
	public bool vulnerableRight;
	public int enemyhealth;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();//used to set parameters in enemy animation tree
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void hit()//call when enemy is hit
	{
		enemyhealth--;//lose one health

	}
}
