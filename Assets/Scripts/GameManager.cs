using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public GameObject mainMenuUI;
	public GameObject mainGameUI;
	public GameObject pauseMenuUI;
	public int goals;
	public  float timeLeft = 10.0f;
	public GUIStyle displayFont;
	private bool bShouldPauseGame = false;

	void Start () 
	{
		displayFont = new GUIStyle();
		displayFont.fontSize = 20;
		mainMenuUI.SetActive(true);
		mainGameUI.SetActive(false);
		pauseMenuUI.SetActive(false);
		PauseGame();
	}

	void Update () 
	{
		if(!bShouldPauseGame)
		{
			timeLeft -= Time.deltaTime;
		}
	}

	public void OnPlayButtonPress()
	{
		ResumeGame();
	}

	public void OnPauseButtonPress()
	{
		PauseGame();
	}
	public void OnResumeButtonPress()
	{
		
	}
	void PauseGame()
	{
		bShouldPauseGame = true;
		Time.timeScale = 0;
	}
	void ResumeGame()
	{
		bShouldPauseGame = false;
		Time.timeScale = 1;
		mainMenuUI.SetActive(false);
	}

	void OnGUI()
	{
		GUI.Label(new Rect(100, 100, 300, 50), "Time Left: " + timeLeft.ToString(), displayFont);
		GUI.Label(new Rect(100, 120, 300, 50), "Goals: " + goals.ToString(), displayFont);

	}

}
