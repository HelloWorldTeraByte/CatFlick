using UnityEngine;
using System.Collections;

public class GoalKeeprsReach : MonoBehaviour 
{
	public GameObject goalKeeper;
	private GoalKeeper goalKeeperScript;
	private GameObject ball; 

	void Start () 
	{
		goalKeeperScript =  goalKeeper.GetComponent<GoalKeeper>();
	}
	void Update()
	{
		if(ball)
			goalKeeperScript.ballsLocation = ball.transform.position;
		else
			goalKeeperScript.ballsLocation = Vector3.zero;
		
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			ball = other.gameObject;
			goalKeeperScript.bMoveKeeper = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			ball = null;
			goalKeeperScript.bMoveKeeper = false;
		}
	}
}
