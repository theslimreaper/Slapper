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
	static float maxHitFlickerTime=.5f;
	static float hitTimer=0f;
	public float flickerDuration=.5f;
	float flickerTimer=0f;
	public Color color0 = Color.red;
	public Color color1 = Color.blue;
	static bool enraged=false;//when the boss is made speed it up
	// Use this for initialization
	void Start () {
		currentTime = directionChangeTimer;
		PointLight.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime-=Time.deltaTime;
		if(currentTime<=0)
		{
			x1=Random.Range (minSpeed,maxSpeed);
			x2=Random.Range (minSpeed,maxSpeed);
			y1=Random.Range (minSpeed,maxSpeed);
			y2=Random.Range (minSpeed,maxSpeed);
			z1=Random.Range (minSpeed,maxSpeed);
			z2=Random.Range (minSpeed,maxSpeed);
			currentTime=directionChangeTimer;
		}
		spotLight1.transform.Rotate (x1, y1, z1);
		spotLight2.transform.Rotate (x2, y2, z2);
		if(hitTimer>0)
		{
			hitTimer -= Time.deltaTime;
			flickerTimer-=Time.deltaTime;
			if(flickerTimer<0)
			{

				if(spotLight1.enabled==true)
				{
				//	print ("turn off the lights");
					spotLight1.enabled=false;
					spotLight2.enabled=false;
				}
				else if(spotLight1.enabled==false)
				{
				//	print ("turn on the lights");
					spotLight1.enabled=true;
					spotLight2.enabled=true;
				}
				else 
					flickerTimer=flickerDuration;
			}
		}
		else
		{
			spotLight1.enabled=true;
			spotLight2.enabled=true;
		}
		if(enraged==true)
			PointLight.color=Color.Lerp(Color.blue, Color.red,1f);
	}

	public static void hit(float percent) {
		print (percent + " % ");
		hitTimer = maxHitFlickerTime;
		if(percent<.35f)
			enraged=true;
	}
}
