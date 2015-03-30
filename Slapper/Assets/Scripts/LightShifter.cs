using UnityEngine;
using System.Collections;

public class LightShifter : MonoBehaviour {
	public Light spotLight1;
	public Light spotLight2;
	public Light PointLight;
	public float directionChangeTimer=5;
	int x1=1,x2=2,y1=1,y2=2,z1=1,z2=2;
	float currentTime;
	public int minSpeed=-2;
	public int maxSpeed=2;
	public Color color0 = Color.red;
	public Color color1 = Color.blue;
	static bool enraged=false;//when the boss is made speed it up
	float temp=0;


	public int totalFlickers=4;//amount of times the light will flicker
	static int currentFlicker=5;
	public float maxFlickerTime=.05f;//amount of time a flicker lasts
	static float currentFlickerTime;
	// Use this for initialization
	void Start () 
	{
		currentTime = directionChangeTimer;
		enraged = false;
		PointLight.color = color1;
		currentFlickerTime = maxFlickerTime;
	}
	
	//fixed update is called at a set delta time
	void FixedUpdate () 
	{
		currentTime-=Time.deltaTime;
		if(currentTime<=0)//change the direction the spotlights are moving 
		{
			x1=Random.Range (minSpeed,maxSpeed);
			x2=Random.Range (minSpeed,maxSpeed);
			y1=Random.Range (minSpeed,maxSpeed);
			y2=Random.Range (minSpeed,maxSpeed);
			z1=Random.Range (minSpeed,maxSpeed);
			z2=Random.Range (minSpeed,maxSpeed);
			currentTime=directionChangeTimer;
		}
		spotLight1.transform.Rotate (x1*Time.timeScale, y1*Time.timeScale, z1*Time.timeScale);//move the spotlights
		spotLight2.transform.Rotate (x2*Time.timeScale, y2*Time.timeScale, z2*Time.timeScale);

		if(enraged==true)//lerp the color when hes enraged
		{
			PointLight.color=Color.Lerp(Color.blue, Color.red,temp);
			temp+=2*Time.deltaTime;
		}
	}
	// Update is called once per frame
	void Update()
	{
		if(currentFlicker<=totalFlickers)//flickers the lights when the enemy is hit
		{
			if(currentFlickerTime<=maxFlickerTime)
				currentFlickerTime+=Time.deltaTime;
			else
			{
				currentFlickerTime=0;
				if(!spotLight1.enabled)
				{
				currentFlicker++;
				spotLight1.enabled=true;
				spotLight2.enabled=true;
				}
				else
				{
					spotLight1.enabled=false;
					spotLight2.enabled=false;
				}
			}
		}
	}

	public static void hit(float percent) 
	{
		currentFlicker = 0;
		if(percent<.35f)
			enraged=true;
	}
}
