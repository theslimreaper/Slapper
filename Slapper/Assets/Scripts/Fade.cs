using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {
	CanvasGroup canvas;
	public float fadeSpeed;
	//on start this script finds the canvasgroup attached to the gameobject and sets the alpha to make it zero
	void Start () {
		canvas=this.gameObject.GetComponent<CanvasGroup>();
		canvas.alpha = 0.0f;
	}
	//each frame the fader script makes the red border that appears when your hit less visible until it is no longer visible
	void Update()
	{
		if (canvas.alpha > 0.0f) //if the border is not invisible
		{
			canvas.alpha -= fadeSpeed * Time.deltaTime;//slowly decreases alpha to make it less visible
		}
	}

}
