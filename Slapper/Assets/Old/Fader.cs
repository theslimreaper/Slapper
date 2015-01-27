using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour {
	public static CanvasGroup canvas;
	public float fadeSpeed;
	public static bool active=false;
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
		else if(canvas.alpha<=0)
			active=false;

	}
	//when a bullet collides with the player make the 'damaged' red border reappear
	public static void resetAlpha()
	{
		canvas.alpha = 1.0f;//reset alpha to 1 to make it 100% visible
		active = true;

	}
	
}
