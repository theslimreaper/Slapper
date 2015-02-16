using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public float smooth = 3.0f;
	private GameObject player;
	private Transform playerT;
	private Vector3 relCameraPos;
	private Vector3 cameraPos;
	private Vector3 camMove;
	private Vector3 startPos;
	private Quaternion cameraRot;
	private Quaternion rotate;
	private Quaternion lRotate;
	private Quaternion rRotate;
	PlayerStatus status;

	void Awake(){
		player = GameObject.FindGameObjectWithTag("Player");
		playerT = player.transform;
		cameraPos = this.transform.position;
		startPos = this.transform.position;
		cameraRot = this.transform.rotation;
		relCameraPos = transform.position - playerT.position;
		camMove = new Vector3(0.2f,0.0f,0.0f);
		lRotate =  Quaternion.Euler(0,10,0)*cameraRot;
		rRotate = Quaternion.Euler(0,-10,0)*cameraRot;
	}

	void FixedUpdate(){
		Vector3 standardPos = playerT.position + relCameraPos;
		Quaternion standardRot = cameraRot;
		Vector3 leftPos = cameraPos - camMove;//playerT.position + Vector3.left * relCameraPosMag;
		Vector3 rightPos = cameraPos + camMove;//playerT.position + Vector3.right * relCameraPosMag;
		status = player.GetComponent<PlayerStatus>();

		if(status.dodgeLeft==true){
			transform.position = Vector3.Lerp(transform.position, leftPos, smooth * Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, lRotate, smooth * Time.deltaTime);
		}
		if(status.dodgeRight==true){
			transform.position = Vector3.Lerp(transform.position, rightPos, smooth * Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, rRotate, smooth * Time.deltaTime);
		}
		if(status.dodgeLeft==false&&status.dodgeRight==false){
			transform.position = standardPos;
			//transform.position = startPos;
			transform.rotation = standardRot;
		}
	}
}