using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	private Vector2 touchStartPos;
	private Vector2 touchEndPos;
	private float kickPower;
	private float kickAngle;
	private float kickPowerMultiplier = 10;
	private float respawnDelay = 0.8f;
	private float destroyDelay = 3.0f;
	private bool bHasCollided = false;
	private bool bHasCollidedWithFloor = false;
	public bool bIsBallKickable;
	public bool bHasBallTriggeredGoal;
	private bool bIsKicked = false;
	private Rigidbody rigidBodyComponent;
	private GameObject instantiatorObject;
	private GameObject gameManager;
	private GameManager gameManagerScript;
	private Instantiator instantiatorScript;
	GUIStyle displayFont;

	void Start () 
	{
		rigidBodyComponent = GetComponent<Rigidbody>();
		bIsBallKickable = true;
		displayFont = new GUIStyle();
		displayFont.fontSize = 20;
		instantiatorObject = GameObject.FindGameObjectsWithTag("Ground")[0];
		gameManager = GameObject.Find("GameManager");
		gameManagerScript = gameManager.GetComponent<GameManager>();
		instantiatorScript = instantiatorObject.GetComponent<Instantiator>();
	}

	void Update () 
	{
		if (Input.touchCount == 1) 
		{
			Touch touch = Input.GetTouch(0);

			switch(touch.phase)
			{
			case TouchPhase.Began:
				
				touchStartPos = TurnVectorToPercentage(touch.position);
				break;

			case TouchPhase.Ended:
				
				touchEndPos = TurnVectorToPercentage(touch.position);
				kickPower = touchEndPos.y - touchStartPos.y;	//Calculate the kickpower depending on the swipe distance
				Kick();
				break;
			}
		}
	}

	//Turn the screen resolution into to percentages for universal support
	Vector2 TurnVectorToPercentage(Vector2 postion)
	{
		Vector2 returnVector;

		returnVector.x = (postion.x / Screen.width) * 100;
		returnVector.y = (postion.y / Screen.height) * 100;

		return returnVector;
	}
	/*
	void OnGUI()
	{
		GUI.Label(new Rect(100, 100, 300, 50), "Kick Angle: " + (kickAngle * 3).ToString(), displayFont);
		GUI.Label(new Rect(100, 120, 300, 50), "Kick Power:" + (kickPower * kickPowerMultiplier).ToString(), displayFont);
	}
	*/
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "BoundaryColliders")
		{
			if(bHasCollided)
				return;
			Destroy(this.gameObject, destroyDelay);
			bHasCollided = true;
		}

	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.name == "Keeper")
		{
			if(bHasCollided)
				return;
			Destroy(this.gameObject, destroyDelay);
			bHasCollided = true;
		}

		if(other.gameObject.tag == "Ground")
		{
			if(bHasCollidedWithFloor)
				return;
			if(bIsKicked)
			{
				rigidBodyComponent.AddForce(-kickAngle * 2, 0, 0);
				bHasCollidedWithFloor = true;
			}
		}
	}

	void Kick()
	{
		if(bIsBallKickable)
		{
			kickAngle = ((Mathf.Atan((touchEndPos.x - touchStartPos.x) / (touchEndPos.y - touchStartPos.y))) * 180) / Mathf.PI;

			if(kickAngle > 0 && kickAngle < 10)
				kickAngle = 0f;
			if(kickAngle < 0 && kickAngle > -10)
				kickAngle = 0f;
			
			if(!float.IsNaN(kickAngle) && !gameManagerScript.bShouldPauseGame)
			{
				rigidBodyComponent.AddForce(kickAngle * 3, kickPower * kickPowerMultiplier , kickPower * kickPowerMultiplier);
				instantiatorScript.SpawnBall(instantiatorScript.respawnLocation, respawnDelay);
				bIsKicked = true;
			}
		}
	}

	public void WindReflect(float windForce)
	{
		rigidBodyComponent.AddRelativeForce(windForce, 0, 0);
	}
}