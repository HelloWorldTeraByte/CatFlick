using UnityEngine;
using System.Collections;

public class GoalLine : MonoBehaviour 
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
				gameManagerScript.goals++;
				other.GetComponent<Ball>().bHasBallTriggeredGoal = true;
			}
		}
	}
}