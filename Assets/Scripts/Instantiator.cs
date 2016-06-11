using UnityEngine;
using System.Collections;

public class Instantiator : MonoBehaviour 
{
	public Vector3 respawnLocation = new Vector3(0.02f, 0.91f, -6.55f);
	private float spawnDealy = 0.5f;
	private GameObject ballInstance;
	public GameObject windSource;
	public GameObject gameManager;
	public GameObject flyObject;
	private int newGoals = 0;
	private int oldGoals = 0;

	void Start () 
	{
		InstantiatBall(respawnLocation);
		windSource.GetComponent<Wind>().ChangeWindSource();
		ChangeLaserLocation();
	}
	void Update()
	{
		newGoals = gameManager.GetComponent<GameManager>().goals;

		if(newGoals != oldGoals)
		{
			ChangeLaserLocation();
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
	void ChangeLaserLocation()
	{
		float splitSpawnerLoc = Random.Range(1f, 3f);
		Vector3 newLocation = new Vector3();

		if(splitSpawnerLoc < 1 &&  splitSpawnerLoc > 0)
		{
			newLocation.x = Random.Range(-3.17f, -1.5f);
			newLocation.y = Random.Range(0.67f , 4.2f);
			newLocation.z = flyObject.transform.position.z;
		}

		if(splitSpawnerLoc < 2 &&  splitSpawnerLoc > 1)
		{
			newLocation.x = Random.Range(1.2f, 3.12f);
			newLocation.y = Random.Range(0.67f , 4.2f);
			newLocation.z = flyObject.transform.position.z;
		}

		if(splitSpawnerLoc < 3 &&  splitSpawnerLoc > 2)
		{
			newLocation.x = Random.Range(-0.84f, 0.69f);
			newLocation.y = Random.Range(1.7f , 4.2f);
			newLocation.z = flyObject.transform.position.z;
		}
		flyObject.transform.position = newLocation;
	}
}