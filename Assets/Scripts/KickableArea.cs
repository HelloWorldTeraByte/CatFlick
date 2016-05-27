using UnityEngine;
using System.Collections;

public class KickableArea : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			other.GetComponent<Ball>().bIsBallKickable = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			other.GetComponent<Ball>().bIsBallKickable = false;
		}	
	}
}