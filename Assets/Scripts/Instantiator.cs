using UnityEngine;
using System.Collections;

public class Instantiator : MonoBehaviour 
{
	public Vector3 respawnLocation = new Vector3(0.02f, 0.91f, -6.55f);
	private float spawnDealy = 0.5f;
	private GameObject ballInstance;
	public GameObject windSource;
	public GameObject gameManager;
	private int newGoals = 0;
	private int oldGoals = 0;

	void Start () 
	{
		InstantiatBall(respawnLocation);
		windSource.GetComponent<Wind>().ChangeWindSource();
	}
	void Update()
	{
		newGoals = gameManager.GetComponent<GameManager>().goals;

		if(newGoals != oldGoals)
		{
			windSource.GetComponent<Wind>().ChangeWindSource();
			oldGoals = newGoals;
		}
	}
	public void DestroyBall(float timeToDestroy)
	{
		Destroy(ballInstance, timeToDestroy);
		//SpawnBall(respawnLocation, spawnDealy);
	}

	public void SpawnBall(Vector3 spawnLocation, float spawnDelay)
	{
		StartCoroutine(InstantiatBallWithDelay(spawnLocation, spawnDealy));
	}

	void InstantiatBall(Vector3 spawnLocation)
	{
		ballInstance = Instantiate(Resources.Load("Prefabs/Ball")) as GameObject;
		ballInstance.transform.position = spawnLocation;
	}

	IEnumerator InstantiatBallWithDelay(Vector3 spawnLocation, float time)
	{
		yield return new WaitForSeconds(time);
		InstantiatBall(spawnLocation);
	}
}