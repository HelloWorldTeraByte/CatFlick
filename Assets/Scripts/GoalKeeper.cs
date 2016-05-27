using UnityEngine;
using System.Collections;

public class GoalKeeper : MonoBehaviour 
{
	public bool bMoveKeeper = false;
	private int keepersSpeed = 45;
	public Vector3 ballsLocation;
	private Rigidbody rigidBodyComponent;

	void Start()
	{
		rigidBodyComponent = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if(bMoveKeeper && ballsLocation != Vector3.zero)
		{
			MoveKeeper();
		}
	}

	public void MoveKeeper()
	{
		rigidBodyComponent.MovePosition(Vector3.MoveTowards(transform.position, 
										new Vector3(ballsLocation.x, ballsLocation.y, 0),
										keepersSpeed * Time.deltaTime));
	}
}
