using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour 
{
	public float windForce = 3.0f;
	public bool rightWindSource = true;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			if(rightWindSource)
				other.GetComponent<Ball>().WindReflect(-windForce);
			if(!rightWindSource)
				other.GetComponent<Ball>().WindReflect(windForce);
		}
	}
}
