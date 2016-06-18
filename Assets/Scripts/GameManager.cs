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
	private AudioSource mainAudioSource;
	public Instantiator instantiator;
	public UIManager uiManager;
	public GameObject mainGameUI;
	public GameObject pauseMenuUI;
	public GameObject gameOverUI;
	public bool bHasGameStarted = false;
	private string savePath = "/SaveData.cat";
	private GameModes currentGameMode;
	public int goals;
	private int flyHitsPrev;
	public int flyHits;
	public int doCatsPerFlyHit = 2;
	private int s30HighScore;
	private int s60HighScore;
	private int s90HighScore;
	private int currentHighScore;
	public  float timeLeft;
	private float gameTime;
	public bool bShouldPauseGame = false;
	public Text goalsText;
	public Text timeLeftText; 
	public Text highScoreText;
	public Text currentScoreText; 
	public Text doCatsScore;
	//public  Animator goalsTextAnimator;
	//public float shakeSpeed;

	void Start () 
	{
		SaveStuff savedData = LoadData();
		bShouldPauseGame = false;
		mainGameUI.SetActive(true);
		pauseMenuUI.SetActive(false);
		gameOverUI.SetActive(false);

		currentGameMode = (GameModes)LoadGameMode();

		switch(currentGameMode)
		{
		case GameModes.TIME30:
			gameTime = 10.0f;
			timeLeft = 10.0f;
			currentHighScore = s30HighScore = savedData.GetBest30sScore();
			break;
		case GameModes.TIME60:
			gameTime = 60.0f;
			timeLeft = 60.0f;
			currentHighScore = s60HighScore = savedData.GetBest60sScore();
			break;
		case GameModes.TIME90:
			gameTime = 90.0f;
			timeLeft = 90.0f;
			currentHighScore = s90HighScore = savedData.GetBest90sScore();
			break;
		default:
			Debug.Log("ERROR: No game mode selected");
			timeLeft = 30.0f;
			break;
		}
		UpdateHUD();	
		mainAudioSource = GetComponent<AudioSource>();
		//goalsTextAnimator = goalsText.GetComponent<Animator>();
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

			if(flyHitsPrev != flyHits)
			{
				instantiator.ChangeLaserLocation();
				uiManager.FadeOutFlyHitText();
				flyHitsPrev = flyHits;
			}
		}

	}

	public void UpdateHUD()
	{
		//goalsTextAnimator.SetFloat("Shake", shakeSpeed);
		if(mainGameUI.activeSelf)
		{
			timeLeftText.text = timeLeft.ToString("F1");
			goalsText.text = goals.ToString();

			float gValue = ScaleBetween(currentHighScore, 0 , 0, 220, goals);
			//mainGameUI.GetComponent<ShakeControl>().shakeSpeed = ScaleBetween(currentHighScore, 0 , 0.5f, 0.8f, goals);

			if(currentHighScore != 0)
				goalsText.color = new Color(1f, gValue/255f, 1f);
			if(currentHighScore < goals)
				goalsText.color = new Color(1f, 1f, 1f);

			if((timeLeft/gameTime * 100) < 25)
			{
				timeLeftText.color = new Color(1f, 0.3f, 0.3f);

			}
		}
	}

	public float ScaleBetween(float oldMin, float oldMax, float newMin, float newMax, float oldValue)
	{
		return (((oldValue - oldMin) * (newMax - newMin)) / (oldMax - oldMin)) + newMin;
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
		mainAudioSource.Pause();
		bShouldPauseGame = true;
	}
	void ResumeGame()
	{
		pauseMenuUI.SetActive(false);
		mainGameUI.SetActive(true);
		gameOverUI.SetActive(false);
		Time.timeScale = 1;
		mainAudioSource.UnPause();
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
		savedData.AddDoCats(flyHits * doCatsPerFlyHit);

		switch(currentGameMode)
		{
		case GameModes.TIME30:
			if(goals > savedData.GetBest30sScore())
			{
				savedData.SetBest30sScore(goals);
			}
			highScoreText.text = savedData.GetBest30sScore().ToString();
			break;

		case GameModes.TIME60:
			if(goals > savedData.GetBest60sScore())
			{
				savedData.SetBest60sScore(goals);
			}
			highScoreText.text = savedData.GetBest60sScore().ToString();
			break;

		case GameModes.TIME90:
			if(goals > savedData.GetBest90sScore())
			{
				savedData.SetBest90sScore(goals);
			}
			highScoreText.text = savedData.GetBest90sScore().ToString();
			break;
			
		}
		Save(savedData);	

		currentScoreText.text = goals.ToString();
		doCatsScore.text = "$" + savedData.GetDoCats().ToString();

	}
	public int LoadGameMode()
	{
		//only do this bit if the file exsists, otherwise it gives an IO exception error
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
			SaveStuff newData = new SaveStuff();		//return  a new set of data if the file is not found
			return newData;
		}
	}

}

//at start we got the highscore
[Serializable]
class SaveStuff
{
	private int best30sScore = 0;
	private int best60sScore = 0;
	private int best90sScore = 0;
	private int docats = 0;

	public void SetBest30sScore(int score)
	{
		best30sScore = score;
	}
	public void SetBest60sScore(int score)
	{
		best60sScore = score;
	}
	public void SetBest90sScore(int score)
	{
		best90sScore = score;
	}
	public void AddDoCats(int amount)
	{
		docats += amount;
	}
	public int GetBest30sScore()
	{
		return best30sScore;
	}
	public int GetBest60sScore()
	{
		return best60sScore;
	}
	public int GetBest90sScore()
	{
		return best90sScore;
	}
	public int GetDoCats()
	{
		return docats;
	}
}
