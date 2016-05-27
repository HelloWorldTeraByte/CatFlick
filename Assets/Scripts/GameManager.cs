using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public GameObject mainMenuUI;
	public GameObject mainGameUI;
	public GameObject pauseMenuUI;

	public int goals;
	public  float timeLeft;
	private bool bShouldPauseGame = false;

	void Start () 
	{
		mainMenuUI.SetActive(true);
		mainGameUI.SetActive(false);
		pauseMenuUI.SetActive(false);

		bShouldPauseGame = true;
		Time.timeScale = 0;
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

	public void OnStartGameButtonPress()
	{
		//new game
		timeLeft = 30.0f;
		Time.timeScale = 1;
		bShouldPauseGame = false;
		mainMenuUI.SetActive(false);
		pauseMenuUI.SetActive(false);
		mainGameUI.SetActive(true);
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
		mainMenuUI.SetActive(false);
		mainGameUI.SetActive(false);
		pauseMenuUI.SetActive(true);
	}
	void ResumeGame()
	{
		bShouldPauseGame = false;
		Time.timeScale = 1;
		mainMenuUI.SetActive(false);
		pauseMenuUI.SetActive(false);
		mainGameUI.SetActive(true);
	}
}
