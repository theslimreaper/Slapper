using UnityEngine;
using System.Collections;

public class ragdoller : MonoBehaviour {

	public GameObject[] JetpackRagdollJoints;
	public Rigidbody[] JetpackRagdollRigids;
	public Quaternion[] baseRotations;
	public Vector3[] basePositions;
	public Quaternion[] currentRotations;
	public Vector3[] currentPositions;

	public float maxTransitionTime=.25f;
	public float transitionTime=0;
	float transitionPercent;
	public bool groundTouched=true;
	// Use this for initialization
	void Start () {
		SetKinematic(true);

		for(int i=0;i<JetpackRagdollJoints.Length;i++)
		{
			JetpackRagdollRigids[i]=JetpackRagdollJoints[i].GetComponent<Rigidbody>();
		}
		SaveBasePosition ();



	}
	
	// Update is called once per frame
	void Update () {

		//lerp from ragdoll to animation beginning

		if(Input.GetKey (KeyCode.Q))
		   {
			SetKinematic(false);
			}
	}
	public void SetKinematic(bool newValue)
	{
		
		for(int i=0;i<JetpackRagdollJoints.Length;i++)
		{
			basePositions[i]=JetpackRagdollJoints[i].transform.localPosition;
			baseRotations[i]=JetpackRagdollJoints[i].transform.rotation;
		}
	}


	//takes starting position of body parts for lerping purposes
	void SaveBasePosition(){
		for(int i=0;i<JetpackRagdollJoints.Length;i++)
		{
			basePositions[i]=JetpackRagdollJoints[i].transform.localPosition;
			baseRotations[i]=JetpackRagdollJoints[i].transform.rotation;
		}
	}

}

