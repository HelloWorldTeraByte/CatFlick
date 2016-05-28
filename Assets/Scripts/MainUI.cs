using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainUI : MonoBehaviour 
{
	public void On30sGameModePressed()
	{
		PlayerPrefs.SetInt("GameMode", 0);
		SceneManager.LoadScene("MainGame");
	}
	public void On60sGameModePressed()
	{
		PlayerPrefs.SetInt("GameMode", 1);
		SceneManager.LoadScene("MainGame");
	}
	public void On90sGameModePressed()
	{
		PlayerPrefs.SetInt("GameMode", 2);
		SceneManager.LoadScene("MainGame");
	}
}
