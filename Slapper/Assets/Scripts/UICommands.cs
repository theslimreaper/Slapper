using UnityEngine;
using System.Collections;

public class UICommands : MonoBehaviour {


	public void restart()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void exitGame()
	{
		Application.Quit();
	}

}
