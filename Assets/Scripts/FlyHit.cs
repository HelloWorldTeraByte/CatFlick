using UnityEngine;
using System.Collections;

public class FlyHit : MonoBehaviour
{
	public GameObject gameMangerObject;
	private GameManager gameManagerScript;

	void Start()
	{
		if(!gameMangerObject)
			Application.Quit();
		gameManagerScript = gameMangerObject.GetComponent<GameManager>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			if(!other.GetComponent<Ball>().bHasBallTriggeredGoal)
			{
				gameManagerScript.flyHits++;
				other.GetComponent<Ball>().bHasBallTriggeredGoal = true;
			}
		}
	}
}
