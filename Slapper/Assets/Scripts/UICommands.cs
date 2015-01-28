using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICommands : MonoBehaviour {
	public Image playerHealth;
	public Image enemyHealth;

	public void restart()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void exitGame()
	{
		Application.Quit();
	}

	public void updatePlayerHealthBar(int maxHealth, int damage)
	{
		playerHealth.fillAmount -= damage / maxHealth;
	}
	public void updateEnemyHealthBar(int maxHealth, int damage)
	{
		enemyHealth.fillAmount -= damage / maxHealth;
	}
}
