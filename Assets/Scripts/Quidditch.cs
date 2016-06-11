using UnityEngine;
using System.Collections;

public class Quidditch: MonoBehaviour 
{
	public GameObject gameMangerObject;
	private GameManager gameManagerScript;
	public int quidditchScoreAmount = 5;

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
				gameManagerScript.goals += quidditchScoreAmount;
				other.GetComponent<Ball>().bHasBallTriggeredGoal = true;
			}
		}
	}
}