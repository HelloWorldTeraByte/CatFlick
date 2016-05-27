using UnityEngine;
using System.Collections;

public class Instantiator : MonoBehaviour 
{
	public Vector3 respawnLocation = new Vector3(0.02f, 0.91f, -6.55f);
	private float spawnDealy = 0.5f;
	private GameObject ballInstance;

	void Start () 
	{
		InstantiatBall(respawnLocation);
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