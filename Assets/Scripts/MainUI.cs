using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainUI : MonoBehaviour 
{
	private string savePath = "GameMode.cat";

	public void On30sGameModePressed()
	{
		SaveGameMode(0);
		SceneManager.LoadScene("MainGame");
	}
	public void On60sGameModePressed()
	{
		SaveGameMode(1);
		SceneManager.LoadScene("MainGame");
	}
	public void On90sGameModePressed()
	{
		SaveGameMode(2);
		SceneManager.LoadScene("MainGame");
	}

	public void SaveGameMode(int gameMode)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + savePath);

		SaveGameMode modeData = new SaveGameMode();
		modeData.gameMode = gameMode;

		binaryFormatter.Serialize(file, modeData);
		file.Close();
	}
}

[Serializable]
class SaveGameMode
{
	public int gameMode;
}