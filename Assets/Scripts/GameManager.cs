using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	enum GameModes {TIME30, TIME60, TIME90};
	public GameObject mainGameUI;
	public GameObject pauseMenuUI;

	public int goals;
	public  float timeLeft;
	public bool bShouldPauseGame = false;
	GUIStyle displayFont;

	void Start () 
	{
		mainGameUI.SetActive(true);
		pauseMenuUI.SetActive(false);
		GameModes currentGameMode = (GameModes)PlayerPrefs.GetInt("GameMode");

		switch(currentGameMode)
		{
		case GameModes.TIME30:
			timeLeft = 30.0f;
			break;
		case GameModes.TIME60:
			timeLeft = 60.0f;
			break;
		case GameModes.TIME90:
			timeLeft = 90.0f;
			break;
		default:
			Debug.Log("ERROR: No game mode selected");
			break;
		}
		displayFont = new GUIStyle();
		displayFont.fontSize = 20;
	}

	void Update () 
	{
		if(!bShouldPauseGame)
		{
			timeLeft -= Time.deltaTime;
			if(timeLeft <= 0)
			{
				PauseGame();
			}
		}
	}
	public void OnPauseButtonPress()
	{
		PauseGame();
	}
	public void OnResumeButtonPress()
	{
		ResumeGame();
	}
	void PauseGame()
	{
		bShouldPauseGame = true;
		Time.timeScale = 0;
		mainGameUI.SetActive(false);
		pauseMenuUI.SetActive(true);
	}
	void ResumeGame()
	{
		pauseMenuUI.SetActive(false);
		mainGameUI.SetActive(true);
		bShouldPauseGame = false;
		Time.timeScale = 1;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(100, 100, 300, 50), "Time Left: " + timeLeft.ToString(), displayFont);
	}
}
