using UnityEngine;
using System.Collections;

public class ShakeControl : MonoBehaviour 
{
	public  Animator goalsTextAnimator;
	public float shakeSpeed;

	void Start ()
	{
		goalsTextAnimator = GetComponent<Animator>();
	}
	
	void Update () 
	{
		goalsTextAnimator.SetFloat("Shake", shakeSpeed);
	}
}
