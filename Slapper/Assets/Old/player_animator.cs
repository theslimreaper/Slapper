using UnityEngine;
using System.Collections;

public class player_animator : MonoBehaviour {

	/* Setup:
	 * 1a 2a 3a 4a 5a
	 * 1b 2b 3b
	 * 
	 * Needs texture changing portion
	 * 
	 * 
	 *
	 */
	public int indexx, indexy;
	public int offsetx, offsety;
	public int[] max;
	public int speed;
	int realspeed;
	public int sheetcount;
	public Texture[] spritesheets;
	int currentsheet = 0;
	float scalex, scaley;
	int step;
	public bool mirrormode = false;
	bool tohold = false;
	bool overridden = false; // If Overridden, ignore sety's until x resets to 0.
	int toconty = -1; // not implimented
	public int mirroringmode = 0;
	int superhold = 0;


	// Use this for initialization
	void Start () {
		indexx = 0;
		indexy = 0;
		step = 0;
		scalex = renderer.material.mainTextureScale.x;
		scaley = renderer.material.mainTextureScale.y;
		renderer.material.mainTexture = spritesheets[0];
		setshader ();
		realspeed = speed;
		//if(mirrormode==true)
		//	transform.localScale = transform.localScale * (-1f);
	}
	
	// Update is called once per frame
	void Update () {
		step += 1;
		if(step>=realspeed)
		{
			step = step - speed;
			indexx += 1;
			if((tohold)&&(indexx==max[indexy]))
			{
				indexx += -1;
				overridden = false;
			}
		}
		if(indexx==max[indexy])
		{
			indexx = 0;
			overridden = false;
		}
		//Debug.Log ("X's: " + indexx + " " + offsetx);
		float newx = (indexx * 1.0f) / (offsetx * 1.0f); // The *1.0f converts to a float
		float newy = ((indexy - currentsheet * offsety) * 1.0f) / (offsety * 1.0f); // automatically.
		//Debug.Log (newx + " " + newy);
		renderer.material.mainTextureOffset = new Vector2(newx, newy);
	}

	public void sety(int newy, bool newmirror)
	{
		if((toconty==-1)&&(!overridden))
		{
			indexy = newy;
			if(indexy>=(offsety*sheetcount))
			{
				indexy = offsety * sheetcount - 1;
			}
			if(indexx>=max[indexy])
			{
				indexx = 0;
			}

			// check which sheet
			int neededsheet = Mathf.FloorToInt( (indexy * 1.0f) / (offsety * 1.0f) );
			if(neededsheet!=currentsheet)
			{
				renderer.material.mainTexture = spritesheets[neededsheet];
				currentsheet = neededsheet;
			}
				
			if(newmirror!=mirrormode)
			{
				mirror ();
			}
			tohold = false;
			renderer.material.mainTextureScale = new Vector2(scalex, scaley);
			realspeed = speed;
		}
	}

	public void setx(int newx)
	{
		indexx = newx;
	}

	public void hold()
	{
		tohold = true;
	}

	public void cont(int conty) // not implimented
	{
		toconty = conty;
	}

	public void override1()
	{
		overridden = true;
	}

	public void unoverride()
	{
		overridden = false;
	}

	public void timerhold(int newtimer) // not implimented
	{

	}

	public void mirror()
	{
		mirrormode = !mirrormode;
		if(mirroringmode==0)
			transform.localScale = transform.localScale * (-1f);
		else
			transform.localScale = new Vector3(transform.localScale.x,
			                                   transform.localScale.y,
			                                   transform.localScale.z * -1f);
	}

	void setshader()
	{
		renderer.material.shader = Shader.Find ("Unlit/Transparent");
	}

	public void setspeed(int newspeed)
	{
		realspeed = newspeed;
	}
}
