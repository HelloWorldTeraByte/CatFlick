using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour 
{
	enum GameModes {TIME30, TIME60, TIME90};
	public GameObject mainGameUI;
	public GameObject pauseMenuUI;
	public GameObject gameOverUI;
	public bool bHasGameStarted = false;
	//private string savePath = "SaveData.cat";
	public int goals;
	public  float timeLeft;
	private float gameTime;
	public bool bShouldPauseGame = false;
	GUIStyle displayFont;
	public Text goalsText;
	public Text timeLeftText;

	void Start () 
	{
		bShouldPauseGame = false;
		mainGameUI.SetActive(true);
		pauseMenuUI.SetActive(false);
		gameOverUI.SetActive(false);

		GameModes currentGameMode = (GameModes)LoadGameMode();

		switch(currentGameMode)
		{
		case GameModes.TIME30:
			gameTime = 30.0f;
			timeLeft = 30.0f;
			break;
		case GameModes.TIME60:
			gameTime = 60.0f;
			timeLeft = 60.0f;
			break;
		case GameModes.TIME90:
			gameTime = 90.0f;
			timeLeft = 90.0f;
			break;
		default:
			Debug.Log("ERROR: No game mode selected");
			timeLeft = 30.0f;
			break;
		}
		displayFont = new GUIStyle();
		UpdateHUD();
	}

	void Update () 
	{
		UpdateHUD();

		if(!bShouldPauseGame && bHasGameStarted)
		{
			timeLeft -= Time.deltaTime;
			if(timeLeft <= 0)
			{
				ShowGameOverScreen();
			}
		}
	}

	public void UpdateHUD()
	{
		timeLeftText.text = timeLeft.ToString("F1");
		goalsText.text = goals.ToString();
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

	public void OnReplayButtonPress()
	{
		/*
		bHasGameStarted = false;
		timeLeft = gameTime;
		goals = 0;
		bShouldPauseGame = false;
		mainGameUI.SetActive(true);
		pauseMenuUI.SetActive(false);
		gameOverUI.SetActive(false);
		bShouldPauseGame = false;
		*/
		SceneManager.LoadScene("MainGame");
	}

	void PauseGame()
	{
		mainGameUI.SetActive(false);
		pauseMenuUI.SetActive(true);
		gameOverUI.SetActive(false);
		Time.timeScale = 0;
		bShouldPauseGame = true;
	}
	void ResumeGame()
	{
		pauseMenuUI.SetActive(false);
		mainGameUI.SetActive(true);
		gameOverUI.SetActive(false);
		Time.timeScale = 1;
		bShouldPauseGame = false;
	}

	void ShowGameOverScreen()
	{
		bHasGameStarted = false;
		pauseMenuUI.SetActive(false);
		mainGameUI.SetActive(false);
		gameOverUI.SetActive(true);
		bShouldPauseGame = true;
	}
	public int LoadGameMode()
	{
		if(File.Exists(Application.persistentDataPath + "GameMode.cat"))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "GameMode.cat", FileMode.Open);
			SaveGameMode modeData = (SaveGameMode)binaryFormatter.Deserialize(file);
			file.Close();

			return modeData.gameMode;
		}
		else
			Debug.Log("No saved game file");
		
		return -1; 
	}

	/*
	public void Save()
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + savePath);

		SaveStuff data = new SaveStuff();
		//save stuff

		binaryFormatter.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + savePath))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + savePath, FileMode.Open);
			SaveStuff data = (SaveStuff)binaryFormatter.Deserialize(file);
			file.Close();

			//load stuff
		}
	}
	*/
}
/*
[Serializable]
class SaveStuff
{
	private int highScore;

	public void SetHighScore(int score)
	{
		highScore = score;
	}
}
*/