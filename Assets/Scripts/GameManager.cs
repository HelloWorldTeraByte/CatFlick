using UnityEngine;
using UnityEngine.SceneManagement;
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
		bShouldPauseGame = false;
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
			timeLeft = 30.0f;
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

	public void OnHomeButtonPress()
	{
		ResumeGame();
		SceneManager.LoadScene("MainMenu");
	}
	 
	void PauseGame()
	{
		mainGameUI.SetActive(false);
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0;
		bShouldPauseGame = true;
	}
	void ResumeGame()
	{
		pauseMenuUI.SetActive(false);
		mainGameUI.SetActive(true);
		Time.timeScale = 1;
		bShouldPauseGame = false;
	}
	/*
	void OnGUI()
	{
		GUI.Label(new Rect(100, 100, 300, 50), "Time Left: " + timeLeft.ToString(), displayFont);
	}
	*/
}
