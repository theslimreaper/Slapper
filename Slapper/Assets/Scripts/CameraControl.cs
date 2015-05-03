using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public float smooth = 5.0f;
	public int rotAngle = 10;
	public float posMove = 0.2f;
	private GameObject player;
	private Transform playerT;
	private Vector3 relCameraPos;
	private Vector3 cameraPos;
	private Vector3 camMove;
	private Vector3 startPos;
	private Quaternion cameraRot;
	private Quaternion startRot;
	private Quaternion rotate;
	private Quaternion lRotate;
	private Quaternion rRotate;
	PlayerStatusWangster status;

	void Awake(){
		player = GameObject.FindGameObjectWithTag("Player");
		playerT = player.transform;
		cameraPos = this.transform.position;
		startPos = this.transform.position;
		startRot = this.transform.rotation;
		cameraRot = this.transform.rotation;
		relCameraPos = transform.position - playerT.position;
		camMove = new Vector3(posMove,0.0f,0.0f);
		lRotate =  Quaternion.Euler(0,rotAngle,0)*cameraRot;
		rRotate = Quaternion.Euler(0,-rotAngle,0)*cameraRot;
	}

	void Update(){
		Vector3 standardPos = playerT.position + relCameraPos;
		Vector3 leftPos = cameraPos - camMove;
		Vector3 rightPos = cameraPos + camMove;
		status = player.GetComponent<PlayerStatusWangster>();
		if(status.dodgeLeft==true){
			transform.position = Vector3.Lerp(transform.position, leftPos, smooth * Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, lRotate, smooth * Time.deltaTime);
		}
		if(status.dodgeRight==true){
			transform.position = Vector3.Lerp(transform.position, rightPos, smooth * Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, rRotate, smooth * Time.deltaTime);
		}
		if(status.dodgeLeft==false&&status.dodgeRight==false){
			transform.position = Vector3.Lerp(transform.position, standardPos, smooth * Time.deltaTime);
			//transform.position = Vector3.Lerp(transform.position, startPos, smooth * Time.deltaTime);
			transform.rotation = Quaternion.Slerp(transform.rotation, startRot, smooth * Time.deltaTime);
		}
	}
}