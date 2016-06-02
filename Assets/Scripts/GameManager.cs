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
	private string savePath = "/SaveData.cat";
	public int goals;
	public  float timeLeft;
	private float gameTime;
	public bool bShouldPauseGame = false;
	public Text goalsText;
	public Text timeLeftText; 
	public Text highScore;
	public Text currentScore; 

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
			gameTime = 10.0f;
			timeLeft = 10.0f;
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
				GameOver();
			}
		}
	}

	public void UpdateHUD()
	{
		if(mainGameUI.activeSelf)
		{
			timeLeftText.text = timeLeft.ToString("F1");
			goalsText.text = goals.ToString();
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

	void GameOver()
	{
		bHasGameStarted = false;
		pauseMenuUI.SetActive(false);
		mainGameUI.SetActive(false);
		gameOverUI.SetActive(true);
		bShouldPauseGame = true;

		SaveStuff savedData = LoadData();

		if(goals > savedData.GetHighScore())
			savedData.SetHighScore(goals);
		
		Save(savedData);
		
		highScore.text = savedData.GetHighScore().ToString();
		currentScore.text = goals.ToString();

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


	void Save(SaveStuff dataToSave)
	{
		FileStream file;
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		file = File.Create(Application.persistentDataPath + savePath);

		binaryFormatter.Serialize(file, dataToSave);
		file.Close();
	}

	SaveStuff LoadData()
	{
		if(File.Exists(Application.persistentDataPath + savePath))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + savePath, FileMode.Open);
			SaveStuff data = (SaveStuff)binaryFormatter.Deserialize(file);
			file.Close();

			return data;
		}
		else
		{
			SaveStuff newData = new SaveStuff();
			return newData;
		}
	}

}

[Serializable]
class SaveStuff
{
	private int highScore = 0;
	private int docats = 0;

	public void SetHighScore(int score)
	{
		highScore = score;
	}
	public void SetCoins(int coins)
	{
		docats = coins;
	}
	public int GetHighScore()
	{
		return highScore;
	}
}
